using Framework.Application;
using Framework.Application.Hashing;
using Marketer.Application.Contract.AI.Account;
using Marketer.Application.Contract.ViewModels.Account;
using Marketer.Domain.Entities.Account;
using Marketer.Domain.RI.Account;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Application
{
    public class OperatorApplication : IOperatorApplication
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IOperatorRepository _operatorRepository;

        public OperatorApplication(IPasswordHasher passwordHasher, IOperatorRepository operatorRepository)
        {
            _passwordHasher = passwordHasher;
            _operatorRepository = operatorRepository;
        }

        public async Task<OperationResult> Create(CreateOperatorVM command)
        {
            OperationResult result = new();

            if (_operatorRepository.Exists(o => o.Mobile == command.Mobile)) return result.Failed(ApplicationMessage.DuplicatedMobile);

            var password = _passwordHasher.Hash(command.Password);

            var user = new Operator(command.RoleId, command.FullName, command.Mobile, password);

            await _operatorRepository.AddEntityAsync(user);
            await _operatorRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Delete(long id)
        {
            OperationResult result = new();

            var user = await _operatorRepository.GetEntityByIdAsync(id);
            if (user is null) result.Failed(ApplicationMessage.UserNotExist);

            user.Delete();
            await _operatorRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Edit(EditOperatorVM command)
        {
            OperationResult result = new();

            var user = await _operatorRepository.GetEntityByIdAsync(command.Id);

            if (user is null) result.Failed(ApplicationMessage.UserNotExist);
            if (_operatorRepository.Exists(o => o.Mobile == command.Mobile && o.Id != command.Id)) return result.Failed(ApplicationMessage.DuplicatedMobile);

            string newPassword = "";

            if (!string.IsNullOrWhiteSpace(command.Password)) newPassword = _passwordHasher.Hash(command.Password);

            user.Edit(command.RoleId, command.FullName, command.Mobile, newPassword);
            await _operatorRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<IEnumerable<OperatorVM>> GetAll() => await _operatorRepository.GetAll();

        public async Task<EditOperatorVM> GetDetailForEditBy(long id) => await _operatorRepository.GetDetailForEditBy(id);
        
    }
}