using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Persistence.Application.Contexts;
using Cemiyet.Persistence.Application.ViewModels;
using Cemiyet.Persistence.Extensions;
using MediatR;

namespace Cemiyet.Application.Genres.Queries.List
{
    public class ListHandler : IRequestHandler<ListQuery, List<GenreViewModel>>
    {
        private readonly AppDataContext _context;

        public ListHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<List<GenreViewModel>> Handle(ListQuery request, CancellationToken cancellationToken)
        {
            var genreSet = await _context.Genres.PagedToListAsync(request.Page, request.PageSize);

            return GenreViewModel.CreateFromGenres(genreSet).ToList();
        }
    }
}
