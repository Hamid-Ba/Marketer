using Framework.Application;
using Framework.Application.Authentication;
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
        private readonly IAuthHelper _authHelper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IOperatorRepository _operatorRepository;

        public OperatorApplication(IAuthHelper authHelper, IPasswordHasher passwordHasher, IOperatorRepository operatorRepository)
        {
            _authHelper = authHelper;
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

        public async Task<OperationResult> Login(LoginOperatorVM command)
        {
            OperationResult result = new();

            var user = await _operatorRepository.GetBy(command.Mobile);

            if (user is null) return result.Failed(ApplicationMessage.UserNotExist);

            if (!_passwordHasher.Check(user.Password, command.Password).Verified) return result.Failed(ApplicationMessage.WrongPassword);

            var userVM = new OperatorAuthViewModel
            {
                Id = user.Id,
                RoleId = user.RoleId,
                Fullname = user.FullName,
                Mobile = user.Mobile
            };

            await _authHelper.SignInAsync(userVM);

            return result.Succeeded();
        }
    }
}