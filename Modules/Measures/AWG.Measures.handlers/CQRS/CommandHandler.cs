using MediatR;
using AWG.Measures.Handlers.Model;

namespace AWG.Measures.Handlers.Command
{
  public class CommandHandler
  {

    private MeasuresContext db;
    private readonly IMediator mediator;

    public CommandHandler(MeasuresContext db, IMediator mediator)
    {
      this.db = db;
      this.mediator = mediator;
    }
  }
}
