using AutoMapper;
using Gym.BL.Mapping;
using Gym.BL.Models.ModelsView;
using Gym.DAL;
using Gym.DAL.DAL.Repositories;
using Gym.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.BL
{
    public class TrainingProgramProcessor
    {
        private readonly IMapper _mapper;
        private readonly TrainingProgramRepository _repository;
        public TrainingProgramProcessor(TrainingProgramRepository repository)//mapper
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfileBL>());
            _mapper = mapperConfig.CreateMapper();
            _repository = repository;
        }

        public bool PutDataIntoDb(TrainingProgramViewModel model)
        {
            TrainingProgramBuilder builder = new(new GetDataFromDAL(new GeneralContext())); //fix!
            TrainingProgramDAL program = builder.GetProgramDAL(model);
            program.Name = model.Name;
            //program.Description = model.Description; //add creator and stuff
            program.IsPublic = model.IsPublic; //use mapper!!
            program.Id = null;
            try
            {
                _repository.Insert(program);
                _repository.Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
