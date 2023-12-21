using Orion.Bussines.Diger;
using Orion.DataAccess;
using Orion.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Bussines
{
    public abstract class  Base<TDto, TSummary, TEntity> : IBase<TDto, TSummary>
        where TEntity : class, IEntity, new()
    {
        protected readonly AppDbContext _context;
        public Base(AppDbContext context)
        {
            _context = context;
        }
        protected abstract TEntity MapEntity(TDto model);
        protected abstract TEntity CreatMapEntity(TDto model);
        protected abstract Expression<Func<TEntity, TDto>> DtoMapper { get; }
        protected abstract Expression<Func<TEntity, TSummary>> SummaryMapper { get; }
        public CommadResult Create(TDto model)
        {
            try
            {
                var entity = MapEntity(model);
                _context.Set<TEntity>().Add(entity);
                _context.SaveChanges();
                return CommadResult.Success();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return CommadResult.Failure();
            }
        }

        public CommadResult Delete(TDto model)
        {
            try
            {
                var entity = MapEntity(model);
                _context.Set<TEntity>().Update(entity);
                _context.SaveChanges();
                return CommadResult.Success();
            }
            catch (Exception ex) 
            {

                Trace.TraceError(ex.ToString());
                return CommadResult.Failure();
            }
        }

        public CommadResult Delete(int Id)
        {
            try
            {
                var entity = new TEntity()
                { Id = Id };
                if (entity != null)
                {
                    _context.Set<TEntity>().Remove(entity);
                    _context.SaveChanges();
                    return CommadResult.Success();
                }
                else
                {
                    return CommadResult.Failure("Kayıt bulunamadı.");
                }

            }
            catch (Exception ex)
            {

                Trace.TraceError(ex.ToString());
                return CommadResult.Failure();
            }
        }

        public IEnumerable<TDto> GetAll()
        {
            try
            {
                return _context.Set<TEntity>()
                    .Select(DtoMapper)
                    .ToList();
            }
            catch (Exception ex)
            {

                Trace.TraceError(ex.ToString());
                return Enumerable.Empty<TDto>();
            }
        }

        public TDto? GetById(int Id)
        {
            try
            {
                //var dtoMapper = new Func<TEntity, TDto>(MapToDto);

                return _context.Set<TEntity>()
                    .Where(entity => entity.Id == Id)
                    .Select(DtoMapper)
                    .SingleOrDefault();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return default;
            }
        }

        public IEnumerable<TSummary> GetSummary()
        {
            try
            {
                //var summaryMapper = new Func<TEntity, TSummary>(MapToSummary);

                return _context.Set<TEntity>()
                    .Select(SummaryMapper)
                    .ToList();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return Enumerable.Empty<TSummary>();
            }
        }

        public TSummary? GetSummaryById(int id)
        {
            try
            {
                return _context.Set<TEntity>()
                    .Where(entity => entity.Id == id)
                    .Select(SummaryMapper)
                    .SingleOrDefault();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return default;
            }
        }

        public CommadResult Update(TDto model)
        {
            try
            {
                var entity = MapEntity(model);
                _context.Set<TEntity>().Update(entity);
                _context.SaveChanges();
                return CommadResult.Success();
            }
            catch (Exception ex)
            {

                Trace.TraceError(ex.ToString());
                return CommadResult.Failure();
            }
        }
    }
}
