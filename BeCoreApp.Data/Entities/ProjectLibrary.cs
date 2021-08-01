using BeCoreApp.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BeCoreApp.Data.Entities
{
    [Table("ProjectLibraries")]
    public class ProjectLibrary : DomainEntity<int>
    {
        public ProjectLibrary() { }
        public ProjectLibrary(int id, string path, string caption, int projectId)
        {
            Id = id;
            Path = path;
            Caption = caption;
            ProjectId = projectId;
        }

        [StringLength(250)]
        public string Path { get; set; }

        [StringLength(250)]
        public string Caption { get; set; }

        public int ProjectId { get; set; }


        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }
    }
}
