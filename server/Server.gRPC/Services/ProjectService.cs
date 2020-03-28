﻿using Google.Protobuf.WellKnownTypes;
using Server.gRPC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DataLibrary.BusinessLogic.ProjectProcessor;

namespace Server.gRPC.Services
{
    public class ProjectService : IProjectService
    {
        public async Task<addProjectResponse> CreateProjectAsync(addProjectParams request)
        {
            CreateProject(request.ProjectId);

            int created = CreateProjectInformation(request.ProjectId, request.OrderId, request.Name,
                       request.Description, request.Manager, request.Client, request.Sector);

            if (created != 0)
            {
                request.ProjectId = 0;
            }
            return new addProjectResponse { ProjectId = request.ProjectId };
        }

        public async Task<Boolean> ProjectExistsAsync(int projectId)
        {
            int total = ProjectExists(projectId);
            return total != 0;
        }

        public async Task<List<projectObject>> GetProjectsAsync(string search)
        {
            List<projectObject> projects = new List<projectObject>();
            var data = LoadProjects(search);

            foreach (var item in data)
            {
                var project = new projectObject();
                project.ProjectId = item.Project_id;
                project.OrderId = item.Order_id;
                project.Name = item.Name;
                project.Description = (!string.IsNullOrEmpty(item.Description)) ? item.Description : "empty";
                project.Manager = item.Manager;
                project.Client = item.Client;
                project.Sector = item.Sector;
                project.CreatedAt = Timestamp.FromDateTime(DateTime.SpecifyKind(item.Created_at, DateTimeKind.Utc));
                project.UpdatedAt = Timestamp.FromDateTime(DateTime.SpecifyKind(item.Updated_at, DateTimeKind.Utc));

                projects.Add(project);
            }
            return projects;
        }

        public async Task<List<projectObject>> GetProjectsByProjectId(int id)
        {
            List<projectObject> projects = new List<projectObject>();
            return projects;
        }
    }
}
