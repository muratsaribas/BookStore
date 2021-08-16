using FluentValidation;

namespace WebApi.BookOperations.UpdateBook{

    public class UpdateBookValidator : AbstractValidator<UpdateBook>
    {
        public UpdateBookValidator(){
            RuleFor(command => command.Id).GreaterThan(0);
            RuleFor(command => command.Model.GenreId).GreaterThan(0);
            RuleFor(command => command.Model.PageCount).GreaterThan(0);
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
        }
    }
}