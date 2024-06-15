using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;

namespace Anchor_Score
{
    internal static class Tools
    {
        private static string ExcelPath(string path)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\config_all\";
            return Path.Combine(desktopPath, path);
        }

        public static void ReadJson()
        {
            string jsonFilePath = @"C:\Users\victor_hash\Source\Repos\Anchor_Score\Anchor.json";

            string jsonContent = File.ReadAllText(jsonFilePath);

            JObject jsonObj = JObject.Parse(jsonContent);

            //定义数据部分
            List<List<string>> body_all = new List<List<string>>();

            //定义数据头
            List<string> header_all = new List<string>()
            {
                "anchorID",
                "all_paid_conversions",
                "all_total_calls",
                "all_effective_answering_rate",
                "all_aib_delivery_times",
                "all_aib_answer_rate",
                "all_call_times",
                "all_call_income_coins",
                "all_gift_income_coins"
            };

            //定义数据部分
            List<List<string>> body = new List<List<string>>();

            //定义数据头
            List<string> header = new List<string>()
            {
                "anchorID",
                "paid_conversions",
                "total_calls",
                "effective_answering_rate",
                "aib_delivery_times",
                "aib_answer_rate",
                "anchor_call_number",
                "call_times",
                "call_income_coins",
                "gift_income_coins"
            };

            foreach (var property in jsonObj)
            {
                // 输出属性名和属性值
                //Console.WriteLine($"Property Name: {property.Key}");
                //Console.WriteLine($"Property Value: {property.Value}");

                //如果类型为数组，说明数据为空，该主播没有数据
                if (property.Value.Type == JTokenType.Array)
                {
                    continue;
                }

                //如果类型为JObject，则需要进行递归遍历
                if (property.Value.Type == JTokenType.Object)
                {
                    JObject obj = (JObject)property.Value;

                    foreach (var jsonItem in obj.Properties())
                    {
                        List<string> body_all_temp = new List<string>();
                        List<string> body_temp = new List<string>();

                        if (jsonItem.Value.Type == JTokenType.Array)
                        {
                            if (jsonItem.Name == "all")
                            {
                                body_all_temp.Add($"{property.Key}");
                                body_all_temp.Add($"{0}");
                                body_all_temp.Add($"{0}");
                                body_all_temp.Add($"{0}");
                                body_all_temp.Add($"{0}");
                                body_all_temp.Add($"{0}");
                                body_all_temp.Add($"{0}");
                                body_all_temp.Add($"{0}");
                                body_all_temp.Add($"{0}");

                                body_all.Add(body_all_temp);
                            }
                            else
                            {
                                body_temp.Add($"{property.Key}");
                                body_temp.Add($"{0}");
                                body_temp.Add($"{0}");
                                body_temp.Add($"{0}");
                                body_temp.Add($"{0}");
                                body_temp.Add($"{0}");
                                body_temp.Add($"{0}");
                                body_temp.Add($"{0}");
                                body_temp.Add($"{0}");
                                body_temp.Add($"{0}");

                                body.Add(body_temp);
                            }
                        }
                        else if (jsonItem.Value.Type == JTokenType.Object)
                        {
                            JObject obj_temp = (JObject)jsonItem.Value;

                            if (jsonItem.Name == "all")
                            {
                                body_all_temp.Add($"{property.Key}");

                                foreach (var jsonDetail_temp in obj_temp)
                                {
                                    body_all_temp.Add($"{jsonDetail_temp.Value}");
                                }

                                body_all.Add(body_all_temp);
                            }
                            else
                            {
                                body_temp.Add($"{property.Key}");

                                foreach (var jsonDetail_temp in obj_temp)
                                {
                                    body_temp.Add($"{jsonDetail_temp.Value}");
                                }

                                body.Add(body_temp);
                            }
                        }
                    }
                }
            }

            //分别写入两个文件夹
            string path_all = ExcelPath(@"data_all.xlsx");
            Write(path_all, header_all, body_all);

            string path = ExcelPath(@"data.xlsx");
            Write(path,header,body);
        }
            //封装方法，写入到excle中
            //excel导出方法
        public static void Write(string filePath, List<string>? header, List<List<string>> data)
            {
                //指定Excel文件路径(绝对路径)
                string excelFilePath = filePath;


                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    ExcelWorksheet sht1 = excelPackage.Workbook.Worksheets.Add("sheet1");

                    if (header == null)
                    {
                        Console.WriteLine("表头数据为空！");
                        return;
                    }

                    //写入表头
                    for (int i = 1; i <= header.Count; i++)
                    {
                        sht1.Cells[1, i].Value = header[i - 1];
                    }

                    //写入表头格式
                    sht1.Row(1).Style.Font.Bold = true;

                    //初始化行数计数器
                    int rowIndex = 2;
                    for (int j = 1; j <= data.Count; j++)
                    {
                        for (int k = 1; k <= data[j - 1].Count; k++)
                        {
                            sht1.Cells[rowIndex, k].Value = data[j - 1][k - 1];
                        }
                        rowIndex++;
                    }

                    //检查当前文档需要改变格式的列数
                    List<int> modifyColumns = ModifyColumns(filePath);

                    if (modifyColumns != null)
                    {
                        //遍历这个列数
                        for (int k = 0; k < modifyColumns.Count; k++)
                        {
                            for (int a = 2; a <= rowIndex; a++)
                            {
                                ExcelRange cell = sht1.Cells[a, modifyColumns[k]];
                                if (cell.Value != null && cell.Value.ToString().Contains("."))
                                {
                                    if (double.TryParse(cell.Value.ToString(), out double numericValue))
                                    {
                                        cell.Value = numericValue;
                                        cell.Style.Numberformat.Format = "0.00";
                                    }
                                }
                                else if (cell.Value != null)
                                {
                                    if (int.TryParse(cell.Value.ToString(), out int numericValue))
                                    {
                                        cell.Value = numericValue;
                                        cell.Style.Numberformat.Format = "0";
                                    }
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                    }

                    //检查Excel表是否存在
                    if (File.Exists(excelFilePath))
                    {
                        Console.WriteLine("当前excel文档已存在，文件删除，重新生成");
                        File.Delete(excelFilePath);
                    }

                    //写入Excel
                    Console.WriteLine("开始生成excel文档");
                    FileInfo fileInfo = new FileInfo(excelFilePath);
                    excelPackage.SaveAs(fileInfo);
                }
            }

            //获取需要修改的Excel的列数
        public static List<int> ModifyColumns(string path)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\config_all\";

            if (path == desktopPath + @"data_all.xlsx")
            {
                return new List<int>() { 1, 2, 3, 4, 5, 6, 7 ,8,9};
            }
            else if(path == desktopPath + @"data.xlsx")
            {
                return new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 ,10};
            }
            return new List<int>() { };
        }
    }
}
