using BusinessObejct.Object;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MemberDAO
    {
            private static MemberDAO instance = null;
            private static readonly object instanceLock = new object();
            public static MemberDAO Instance
            {
                get
                {
                    lock (instanceLock)
                    {
                        if (instance == null) 
                        { 
                            instance = new MemberDAO(); 
                        }
                        return instance;
                    }
                }
            }

            public IEnumerable<Member> GetMemberList()
            {
                List<Member> members = new List<Member>();
                try
                {
                    using (var context = new SaleManagementContext())
                    {
                        members = context.Members.ToList();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return members;
            }

            public Member GetMemberByID(int memberID)
            {
                Member member = null;
                try
                {
                    using (var context = new SaleManagementContext())
                    {
                        member = context.Members.SingleOrDefault(m => m.MemberId == memberID);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return member;
            }
            public void AddNew(Member member)
            {
                try
                {
                    Member _member = GetMemberByID(member.MemberId);
                    if (_member == null)
                    {
                        using (var context = new SaleManagementContext())
                        {
                            context.Members.Add(member);
                            context.SaveChanges();
                        }
                    }
                    else 
                    { 
                        throw new Exception("The member is already existed!"); 
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            public void Update(Member member)
            {
                try
                {
                    Member _member = GetMemberByID(member.MemberId);
                    if (_member != null)
                    {
                        using (var context = new SaleManagementContext())
                        {
                            context.Members.Update(member);
                            context.SaveChanges();
                        }
                    }
                    else 
                    { 
                        throw new Exception("The member does not exist!"); 
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            public void Remove(int memberId)
            {
                try
                {
                    Member member = GetMemberByID(memberId);
                    if (member != null)
                    {
                        using (var context = new SaleManagementContext())
                        {
                            context.Members.Remove(member);
                            context.SaveChanges();

                        }
                    }
                    else 
                    { 
                        throw new Exception("The member does not exist!"); 
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            public Member Login(string email, string password)
            {
                Member member = null;
                try
                {
                    using (var context = new SaleManagementContext())
                    {
                        member = context.Members.SingleOrDefault(mem => mem.Email.ToLower().Equals(email) && mem.Password.Equals(password));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return member;
            }


            public IEnumerable<Member> Filter(List<Member> members, string country, string city)
            {
                if (country.Equals("Country")) 
                { 
                    return members; 
                }

                if (city.Equals("City"))
                {
                    List<Member> result = new List<Member>();
                    for (int i = 0; i < members.Count; i++)
                    {
                        if (members.ElementAt(i).Country.Equals(country)) 
                        { 
                            result.Add(members.ElementAt(i));
                        } 
                    }
                    return result;
                }
                else
                {
                    List<Member> result = new List<Member>();
                    for (int i = 0; i < members.Count; i++)
                    {
                        if (members.ElementAt(i).Country.Equals(country) && members.ElementAt(i).City.Equals(city)) 
                        { 
                            result.Add(members.ElementAt(i));
                        }
                    }
                    return result;
                }
            }
            public IEnumerable<Member> Search(string searchValue)
            {
                var result = new List<Member>();
                try
                {
                    using (var context = new SaleManagementContext())
                    {
                        var members = from mem in context.Members
                                      where mem.Email.ToLower().Contains(searchValue) || mem.MemberId.ToString().Contains(searchValue)
                                      select mem;
                        result = members.ToList<Member>();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return result;
            }


        }
}
