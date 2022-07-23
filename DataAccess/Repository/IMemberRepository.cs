using BusinessObejct.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        IEnumerable<Member> GetMemberList();
        Member GetMemberByID(int memberID);
        Member Login(string email, string password);
        void InsertMember(Member member);
        void DeleteMember(int memberID);
        void UpdateMember(Member member);
        IEnumerable<Member> Search(string searchValue);
    }
}
