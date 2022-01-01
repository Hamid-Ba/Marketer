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
    public class VisitorApplication : IVisitorApplication
    {
        private readonly IAuthHelper _authHelper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IVisitorRepository _visitorRepository;

        public VisitorApplication(IAuthHelper authHelper, IPasswordHasher passwordHasher, IVisitorRepository visitorRepository)
        {
            _authHelper = authHelper;
            _passwordHasher = passwordHasher;
            _visitorRepository = visitorRepository;
        }

        public async Task<OperationResult> BlockProcess(long id)
        {
            OperationResult result = new();

            var visitor = await _visitorRepository.GetEntityByIdAsync(id);
            if (visitor is null) return result.Failed(ApplicationMessage.UserNotExist);

            if (visitor.IsBlock) visitor.BlockPrccess(false);
            else visitor.BlockPrccess(true);

            await _visitorRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Create(CreateVisitorVM command)
        {
            OperationResult result = new();

            if (_visitorRepository.Exists(v => v.Mobile == command.Mobile || v.UniqueCode == command.UniqueCode))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            var password = _passwordHasher.Hash(command.Password);

            var visitor = new Visitor(command.UniqueCode, command.FullName, command.Mobile, password);

            await _visitorRepository.AddEntityAsync(visitor);
            await _visitorRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Delete(long id)
        {
            OperationResult result = new();

            var visitor = await _visitorRepository.GetEntityByIdAsync(id);
            if (visitor is null) return result.Failed(ApplicationMessage.UserNotExist);

            visitor.Delete();
            await _visitorRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Edit(EditVisitorVM command)
        {
            OperationResult result = new();

            var visitor = await _visitorRepository.GetEntityByIdAsync(command.Id);

            if (visitor is null) return result.Failed(ApplicationMessage.UserNotExist);
            if (_visitorRepository.Exists(v => (v.Mobile == command.Mobile || v.UniqueCode == command.UniqueCode) && v.Id != command.Id))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            string newPassword = "";

            if (!string.IsNullOrWhiteSpace(command.Password))
                newPassword = _passwordHasher.Hash(command.Password);

            visitor.Edit(command.UniqueCode, command.FullName, command.Mobile, newPassword);
            await _visitorRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<IEnumerable<VisitorVM>> GetAll() => await _visitorRepository.GetAll();

        public async Task<EditVisitorVM> GetDetailForEditBy(long id) => await _visitorRepository.GetDetailForEditBy(id);

        public async Task<OperationResult> Login(LoginVisitorVM command)
        {
            OperationResult result = new();

            var visitor = await _visitorRepository.GetBy(command.Mobile);

            if (visitor is null) return result.Failed(ApplicationMessage.UserNotExist);

            if (!_passwordHasher.Check(visitor.Password, command.Password).Verified) return result.Failed(ApplicationMessage.WrongPassword);

            var authVM = new VisitorAuthViewModel
            {
                Id = visitor.Id,
                Fullname = visitor.FullName,
                Code = visitor.UniqueCode,
                Mobile = visitor.Mobile
            };

            _authHelper.SignInAsync(authVM);

            return result.Succeeded();
        }
    }
}