using Application.DTOs.Products;
using AutoMapper;
using Domain.Repositories.Products;
using MediatR;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Inventories;
using Domain.Repositories.Inventories;
using Domain.Entities;

namespace Application.UseCases.Inventories.Queries.Handlers
{
    public class GetInventoriesQueryHandler : IRequestHandler<GetInventoriesQuery, IEnumerable<InventoryResponse>>
    {
        private readonly IInventoriesReaderRepository _inventoriesReaderRepository;
        private readonly IMapper _mapper;

        public GetInventoriesQueryHandler(IInventoriesReaderRepository inventoriesReaderRepository,
            IMapper mapper)
        {
            _inventoriesReaderRepository = inventoriesReaderRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<InventoryResponse>> Handle(GetInventoriesQuery request, CancellationToken cancellationToken)
        {
            var stockItems = await _inventoriesReaderRepository.MultipleByValue(request.SkuCodes);

            return _mapper.Map<IEnumerable<InventoryResponse>>(stockItems);
        }
    }
}
