using BusinessObejct.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public void DeleteMember(int memberID) => MemberDAO.Instance.Remove(memberID);

        public Member GetMemberByID(int memberID) => MemberDAO.Instance.GetMemberByID(memberID);

        public IEnumerable<Member> GetMemberList() => MemberDAO.Instance.GetMemberList();

        public void InsertMember(Member member) => MemberDAO.Instance.AddNew(member);

        public Member Login(string email, string password) => MemberDAO.Instance.Login(email, password);

        public IEnumerable<Member> Search(string searchValue) => MemberDAO.Instance.Search(searchValue);

        public void UpdateMember(Member member) => MemberDAO.Instance.Update(member);
    }
}
