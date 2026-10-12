using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UW.AdminPagoExpress.Shared.Dtos;
using UW.AdminPagoExpress.Shared.Request;

namespace UW.AdminPagoExpress.Shared.Interfaces;

public interface IOCIService
{
    Task<List<OCIDto>> GetListOfConsult(OCIRequest request);
}
