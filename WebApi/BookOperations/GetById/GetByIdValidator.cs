using FluentValidation;

namespace WebApi.BookOperations.GetById{

    public class GetByIdValidator : AbstractValidator<GetById>
    {
        public GetByIdValidator(){
            RuleFor(command => command.Id).GreaterThan(0);
        }
    }

}