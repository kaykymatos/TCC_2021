using FluentValidation.Results;
using System;
using TCCESTOQUE.Models;
using TCCESTOQUE.ViewModel;

namespace TCCESTOQUE.Interfaces.Service
{
    public interface IClienteService : IBaseService<ClienteModel>
    {

        public ValidationResult PostCriacao(ClienteViewModel cliente);

        public ClienteViewModel GetEdicao(Guid? id);

        public ValidationResult PutEdicao(ClienteViewModel cliente);

        public bool PostExclusao(Guid id);
    }
}
