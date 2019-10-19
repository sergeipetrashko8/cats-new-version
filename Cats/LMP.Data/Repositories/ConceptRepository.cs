﻿using System.Collections.Generic;
using System.Linq;
using Application.Core.Data;
using LMP.Data.Infrastructure;
using LMP.Data.Repositories.RepositoryContracts;
using LMP.Models;
using Microsoft.EntityFrameworkCore;

namespace LMP.Data.Repositories
{
    public class ConceptRepository : RepositoryBase<LmPlatformModelsContext, Concept>, IConceptRepository
    {
        public ConceptRepository(LmPlatformModelsContext dataContext)
            : base(dataContext)
        {
        }

        public Concept GetTreeConceptByElementId(int id)
        {
            using var context = new LmPlatformModelsContext();

            var concept = context.Set<Concept>().Include(c => c.Author).Include(c => c.Subject)
                .Include(c => c.Children).Include(s => s.Subject).FirstOrDefault(e => e.Id == id);
            InitTreeConceptByParentIdInner(concept, concept.Children);
            return concept;
        }

        public Concept GetById(int id)
        {
            using var context = new LmPlatformModelsContext();

            var concept = context.Set<Concept>()
                .Include(c => c.Children)
                .Include(c => c.Author)
                .Include(c => c.Subject)
                .FirstOrDefault(e => e.Id == id);
            return concept;
        }

        public IEnumerable<Concept> GetRootElementsByAuthorId(int authorId)
        {
            using var context = new LmPlatformModelsContext();

            var concepts = context.Set<Concept>()
                .Include(c => c.Children)
                .Include(a => a.Author)
                .Include(c => c.Subject)
                .Include(c => c.Subject.SubjectModules)
                .Where(e => e.Author.Id == authorId && !e.ParentId.HasValue && e.IsGroup);
            return concepts.ToList();
        }


        public IEnumerable<Concept> GetByParentId(int parentId)
        {
            using var context = new LmPlatformModelsContext();
            
            var concepts = context.Set<Concept>().Include(c => c.Children).Include(s => s.Subject)
                .Where(e => e.ParentId == parentId);
            return concepts.ToList();
        }

        public IEnumerable<Concept> GetBySubjectId(int subjectId)
        {
            using var context = new LmPlatformModelsContext();
            
            var concepts = context.Set<Concept>().Include(c => c.Children).Include(s => s.Subject)
                .Where(e => e.Subject.Id == subjectId);
            return concepts.ToList();
        }

        public IEnumerable<Concept> GetByAuthorId(int authorId)
        {
            using var context = new LmPlatformModelsContext();
            
            var concepts = context.Set<Concept>().Include(c => c.Children).Include(s => s.Author)
                .Include(s => s.Subject).Where(e => e.Author.Id == authorId);
            return concepts.ToList();
        }

        public void Remove(int id, bool removeChildren)
        {
            using var context = new LmPlatformModelsContext();
            
            var concept = removeChildren ? GetTreeConceptByElementId(id) : GetById(id);
            if (removeChildren)
                RemoveChildren(concept, concept.Children);
            else
                Delete(concept);
        }


        public IEnumerable<Concept> GetRootElementsBySubjectId(int subjectId)
        {
            using var context = new LmPlatformModelsContext();
            
            var concepts = context.Set<Concept>().Include(c => c.Children).Include(a => a.Subject)
                .Where(e => e.Subject.Id == subjectId && !e.ParentId.HasValue && e.IsGroup);
            return concepts.ToList();
        }

        private void InitTreeConceptByParentIdInner(Concept parent, IEnumerable<Concept> childrens)
        {
            if (parent.Children.Any())
                foreach (var child in parent.Children)
                    InitTreeConceptByParentIdInner(child, child.Children.ToList());
            else
                return;
        }

        private void RemoveChildren(Concept parent, IEnumerable<Concept> children)
        {
            Delete(parent);
            if (children != null && children.Any())
                foreach (var c in children)
                    RemoveChildren(c, c.Children);
            else
                return;
        }
    }
}