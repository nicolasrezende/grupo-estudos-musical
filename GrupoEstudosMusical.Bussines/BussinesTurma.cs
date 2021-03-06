﻿using GrupoEstudosMusical.Bussines.Exceptions;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Bussines
{
    public class BussinesTurma : BussinesGeneric<Turma>, IBussinesTurma
    {
        private readonly IRepositoryTurma _repositoryTurma;
        private readonly IBussinesMatricula _bussinesMatricula;
        public BussinesTurma(IRepositoryTurma repositoryTurma, IBussinesMatricula bussinesMatricula) : base(repositoryTurma)
        {
            _repositoryTurma = repositoryTurma;
            _bussinesMatricula = bussinesMatricula;
        }

        public async override Task InserirAsync(Turma entity)
        {
            VerificaDatas(entity.DataInicio, entity.TerminoAula);
            VerificaExistenciaDaTurmaPorNomePeriodoSemestre(entity.Nome, entity.Periodo, entity.Semestre, entity.Id);
            VerificaCampoQuantidadeDeAlunos(entity);
            await base.InserirAsync(entity);
        }
        public void VerificaCampoQuantidadeDeAlunos(Turma entity)
        {
            if (entity.QuantidadeAlunos <= 0)
                throw new CrudTurmaException("A quantidade de alunos na turma não pode ser nferior a 1");
        }
        public void VerificaDatas(DateTime dataInicio, DateTime terminoAula)
        {
            if (dataInicio > terminoAula)
            {
                throw new CrudTurmaException("A data de início não pode ser superior do término das aulas!");
            }
        }
        public async override Task AlterarAsync(Turma entity)
        {
            VerificaDatas(entity.DataInicio, entity.TerminoAula);
            VerificaExistenciaDaTurmaPorNomePeriodoSemestre(entity.Nome, entity.Periodo, entity.Semestre, entity.Id);
            VerificaCampoQuantidadeDeAlunos(entity);
            await base.AlterarAsync(entity);
        }
        public void VerificaExistenciaDaTurmaPorNomePeriodoSemestre(string nomeTurma, int periodo, int semestre, int Id)
        {
            var turmaFiltrada = _repositoryTurma.VerificarExistenciaDaTurmaPorNomePeriodoSemestre(nomeTurma, periodo, semestre, Id);

            if (turmaFiltrada != null)
            {
                throw new CrudTurmaException($"Já existe uma turma com este nome, vinculada ao {semestre}º semestre do período de {periodo}");
            }
        }

        public async Task<List<Turma>> ObterTurmasDoAluno(int IdAluno)
        {
            var matriculas = await _bussinesMatricula.ObterMatriculasPorAluno(IdAluno);

            var turmas = new List<Turma>();

            foreach(var matricula in matriculas)
            {
                turmas.Add(new Turma() { Id = matricula.TurmaId, Nome = matricula.Turma.Nome });
            }

            return turmas;
        }

        public IList<Turma> ObterTurmasAtivasPorModulo(int moduloId) => _repositoryTurma.ObterTurmasAtivasPorModulo(moduloId);
    }
}
