using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Application
{
    public interface ITokenStorage
    {
        bool Exists(Guid tokenId);
        void Add(Guid tokenId);
        void Remove(Guid tokenId);
    }
}
