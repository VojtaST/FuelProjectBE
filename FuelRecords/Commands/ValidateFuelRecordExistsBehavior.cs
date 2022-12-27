using FuelProject.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FuelProject.FuelRecords.Commands
{
    public class ValidateFuelRecordExistsBehavior<TRequest> : IPipelineBehavior<TRequest, ActionResult>
        where TRequest : IRequest<ActionResult>, IFuelRecordById

    {
        private readonly IFuelRecordRepository _repository;

        public ValidateFuelRecordExistsBehavior(IFuelRecordRepository repository)
        {
            _repository = repository;
        }

        public async Task<ActionResult> Handle(TRequest request, RequestHandlerDelegate<ActionResult> next, CancellationToken cancellationToken)
        {
            if ((await _repository.Get(request.Id)) is null)
            {
                return new NotFoundObjectResult("FuelRecord not found");
            }

            return await next();
        }
    }
}
