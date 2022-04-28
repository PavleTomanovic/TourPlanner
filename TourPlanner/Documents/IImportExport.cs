using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DTO;

namespace TourPlanner.Documents
{
    public interface IImportExport
    {
        HttpResponseDTO ImportFile(string filename);
        void ExportFile(string filename, HttpResponseDTO httpResponseDTO);
    }
}
