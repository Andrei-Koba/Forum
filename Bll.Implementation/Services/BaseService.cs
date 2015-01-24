using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bll.Interface;
using Dal.Interface.DataAccess;
using Bll.Implementation.EntityMappers;
using Interfaces;
using Dal.Interface;

namespace Bll.Implementation.Services
{
    public class BaseService<TDal, TBll, TRepository, TMapper> : IService<TBll>
        where TBll :class, IEntity
        where TDal :class, IEntity
        where TRepository : IEntityRepository<TDal>
        where TMapper : IEntityMapper<TBll, TDal>, new()
    {
        protected readonly TRepository _repository;
        protected readonly TMapper _mapper;
        protected readonly IUnitOfWork _unitOfWork;

        public BaseService(TRepository repository, IUnitOfWork uow)
        {
            if (repository == null) throw new ArgumentNullException("Repository is null");
            if (uow == null) throw new ArgumentNullException("UnitOfWork is null");
            _repository = repository;
            _unitOfWork = uow;
            _mapper = new TMapper();
        }

        public IEnumerable<TBll> GetAll()
        {
            IEnumerable<TDal> dal = _repository.GetAll().ToList();
            if (dal.Count() == 0) return new List<TBll>();
            IEnumerable<TBll> bll = dal.Select(_mapper.GetBll);
            return bll;
        }

        public TBll GetById(long id)
        {
            TDal dal = _repository.FindById(id);
            if (dal == null) return null;
            return _mapper.GetBll(dal);
        }

        public void Add(TBll entity)
        {
            try
            {
                _repository.Add(_mapper.GetDal(entity));
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw new ServiceException("Can't add "+typeof(TBll).Name+" item to database", e);
            }
        }

        public void Edit(TBll entity)
        {
            try
            {
                _repository.Edit(_mapper.GetDal(entity));
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw new ServiceException("Can't edit " + typeof(TBll).Name + " item in database", e);
            }
        }

        public void Delete(TBll entity)
        {
            try
            {
                _repository.Delete(_mapper.GetDal(entity));
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw new ServiceException("Can't delete " + typeof(TBll).Name + " item from database", e);
            }
        }

        public void Save()
        {
            _repository.Save();
        }
    }
}
