using Application.Interface;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features;

public abstract class BaseFeature : IRequest<IResponse>
{
    public abstract class BaseHandler<T> : IRequestHandler<T, IResponse> where T : BaseFeature
    {
        protected IApplicationDbContext _context;

        protected BaseHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public abstract Task<IResponse> Handle(T request, CancellationToken cancellationToken);
    }
}

public abstract class BaseQuery<T, E> : IRequest<T>
{
    public abstract class BaseHandler<E> : IRequestHandler<E, T> where E : BaseQuery<T, E>
    {
        protected IApplicationDbContext _context;
        protected IHttpContextAccessor _httpContextAccessor;
        protected BaseHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public abstract Task<T> Handle(E query, CancellationToken cancellationToken);
    }
}