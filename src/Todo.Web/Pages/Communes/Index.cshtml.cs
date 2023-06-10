﻿using AutoMapper.Internal.Mappers;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectMapping;
using OfficeOpenXml;
using Todo.Communes;

namespace Todo.Web.Pages.Communes
{
    public class IndexModel : TodoPageModel
    {
        private readonly ICommuneAppService _communeAppService;

        public IndexModel(ICommuneAppService communeAppService)
        {
            _communeAppService = communeAppService;
        }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostExportCSVAsync()
        {
            var result = await _communeAppService.GetListAsync();
            var exportResult = new PagedResultDto<ExportCommuneDto>
            {
                TotalCount = result.TotalCount,
                Items = ObjectMapper.Map<List<CommuneDto>, List<ExportCommuneDto>>(result.Items.ToList())
            };
            var memoryStream = new MemoryStream();
            var writer = new StreamWriter(memoryStream, Encoding.UTF8);
            var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csvWriter.WriteRecords(exportResult.Items);
            writer.Flush();
            memoryStream.Position = 0;
            var fileDownloadName = "Communes.csv";
            return File(memoryStream, "text/csv", fileDownloadName);
        }

        public async Task<IActionResult> OnPostExportExcelAsync()
        {
            var result = await _communeAppService.GetListAsync();
            var exportResult = new PagedResultDto<ExportCommuneDto>
            {
                TotalCount = result.TotalCount,
                Items = ObjectMapper.Map<List<CommuneDto>, List<ExportCommuneDto>>(result.Items.ToList())
            };

            // Tạo một gói Excel mới
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Communes");

            // Điền dữ liệu vào trang tính
            worksheet.Cells["A1"].LoadFromCollection(exportResult.Items, true);

            // Lưu tệp Excel vào bộ nhớ đệm và trả về tệp Excel dưới dạng phản hồi HTTP
            var memoryStream = new MemoryStream();
            package.SaveAs(memoryStream);
            memoryStream.Position = 0;
            var fileDownloadName = "Communes.xlsx";
            return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileDownloadName);
        }
    }
}
