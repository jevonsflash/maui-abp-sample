﻿using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace MauiBoilerplate.Core.Entities
{
    public class Song : FullAuditedEntity<long>, IValidatableObject
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override long Id { get; set; }

        [Required(ErrorMessage ="歌曲名称必须填写")]
        [StringLength(6, ErrorMessage = "歌曲名称要在6个字以内")]
        public string MusicTitle { get; set; }

        [Required(ErrorMessage = "艺术家必须填写")]
        [StringLength(10, ErrorMessage = "艺术家要在10个字以内")]
        public string Artist { get; set; }

        [Required(ErrorMessage = "专辑名必须填写")]
        [StringLength(10, ErrorMessage = "专辑名要在10个字以内")]
        public string Album { get; set; }

        public TimeSpan Duration { get; set; }
        public DateTime ReleaseDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ReleaseDate != default && ReleaseDate>DateTime.Now)
            {
                yield return new ValidationResult("ReleaseDate不能大于当天",
                                  new[] { nameof(ReleaseDate) });
            }

        }
    }
}
