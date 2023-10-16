﻿using Blog.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Repositories.Interfaces
{
    public interface ITagPostsRepository : IGenericRepository<TagPosts>
    {
        public void AddTagsToPost(IList<TagPosts> tagPosts);


    }
}
