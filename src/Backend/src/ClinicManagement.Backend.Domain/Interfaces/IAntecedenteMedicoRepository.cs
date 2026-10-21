using ClinicManagement.Backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicManagement.Backend.Domain.Interfaces
{
    public interface IAntecedenteMedicoRepository
    {
        public void Remove(AntecedenteMedico antecedente);
    }
}
