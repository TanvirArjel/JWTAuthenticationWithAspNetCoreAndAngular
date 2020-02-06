﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWTAuthenticationWithAspNetCoreAndAngular.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JWTAuthenticationWithAspNetCoreAndAngular.Data.EntityConfigurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasIndex(au => au.UserName).IsUnique(true);
            builder.HasIndex(au => au.NormalizedUserName).IsUnique(true);
            builder.HasIndex(au => au.Email).IsUnique(true);
            builder.HasIndex(au => au.NormalizedEmail).IsUnique(true);
        }
    }
}
