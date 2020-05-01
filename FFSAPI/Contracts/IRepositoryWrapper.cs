using System;
using System.Collections.Generic;
using FFSAPI.Repository;
using FFSAPI.Models;

namespace FFSAPI.Contracts
{
    public interface IRepositoryWrapper
    {
        //implementerar en referens till alla models?
        IRepositoryBase<Movie> Movie { get; }
        IRepositoryBase<Studio> Studio { get; }
        IRepositoryBase<Trivia> Trivia { get; }
        IRepositoryBase<Movie_Studio> Movie_Studio { get; }
        void Save();

    }
}
