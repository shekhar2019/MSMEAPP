using Portal.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Portal.DAL
{
    public partial class WelcomeHome
    {
        public string UserName { get; set; }
    }
    public partial class ContactInfo
    {
        public string UserName { get; set; }
    }
    public partial class SubMenu
    {
        public string MenuName { get; set; }
    }
    public partial class SubChildMenu
    {
        public string SubMenuName { get; set; }
    }
    public partial class DBInterface : IDisposable
    {
        public ResponseOut AddEditWelcomeHome(WelcomeHome welcomeHome)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                
                if (entities.WelcomeHomes.Any(x => x.WelcomeHomeId == welcomeHome.WelcomeHomeId && welcomeHome.WelcomeHomeId == 0))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateWelcomeHome;
                }
                else
                {
                    if (welcomeHome.WelcomeHomeId == 0)
                    {
                        welcomeHome.CreateDate = DateTime.Now;
                        entities.WelcomeHomes.Add(welcomeHome);
                        responseOut.message = ActionMessage.WelcomeHomeCreatedSuccess;
                    }
                    else
                    {
                        welcomeHome.ModifyBy = welcomeHome.ModifyBy;
                        welcomeHome.ModifyDate = welcomeHome.ModifyDate;
                        entities.WelcomeHomes.Where(x => x.WelcomeHomeId == welcomeHome.WelcomeHomeId).ToList().ForEach(x =>
                        {
                            x.WelcomeHomeId = welcomeHome.WelcomeHomeId;
                            x.Title = welcomeHome.Title;
                            x.WelcomeContent = welcomeHome.WelcomeContent;
                            x.ModifyBy = welcomeHome.ModifyBy;
                            x.ModifyDate = welcomeHome.ModifyDate;
                            x.WelcomeHomeStatus = welcomeHome.WelcomeHomeStatus;
                        });
                        responseOut.message = ActionMessage.WelcomeHomeUpdatedSuccess;
                    }

                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                var errorList = new List<string>();

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorList.Add(String.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage));
                    }
                }

            }
            return responseOut;
        }

        public List<WelcomeHome> GetWelcomeHomeList()
        {
            List<WelcomeHome> welcomeList = new List<WelcomeHome>();
            try
            {
                var welcomeHome = entities.WelcomeHomes.Join(entities.Users,
                    w => w.CreatedBy,
                    u => u.UserId, (w, u) => new
                    {
                        WelcomeHomeId = w.WelcomeHomeId,
                        Title = w.Title,
                        WelcomeContent = w.WelcomeContent,
                        WelcomeHomeStatus = w.WelcomeHomeStatus,
                        CreateDate = w.CreateDate,
                        UserName = u.UserName
                    }).ToList();

                if (welcomeHome != null && welcomeHome.Count > 0)
                {
                    foreach (var item in welcomeHome)
                    {
                       
                            welcomeList.Add(new WelcomeHome { WelcomeHomeId = item.WelcomeHomeId, Title = item.Title, WelcomeContent = item.WelcomeContent, WelcomeHomeStatus = item.WelcomeHomeStatus, UserName = item.UserName });
                        
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return welcomeList;
        }

        public ResponseOut RemoveWelcomeHome(int welcomeHomeId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                var welcome = entities.WelcomeHomes.FirstOrDefault(x => x.WelcomeHomeId == welcomeHomeId);
                entities.WelcomeHomes.Remove(welcome);
                entities.SaveChanges();
                responseOut.status = ActionStatus.Success;
                responseOut.message = ActionMessage.RemoveWelcomeHome;
            }
            catch (Exception ex)
            {
                responseOut.status = ActionStatus.Fail;
                responseOut.message = ActionMessage.ApplicationException;
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;
        }

    }
}
