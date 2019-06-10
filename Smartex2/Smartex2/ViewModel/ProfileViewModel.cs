using Smartex.Annotations;
using Smartex.Exception;
using Smartex.Model;
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Smartex.ViewModel
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        #region fields

        private UserPersonalInfo _userPersonalInfo;

        public UserPersonalInfo UserPersonalInfo
        {
            get { return _userPersonalInfo; }
            set
            {
                _userPersonalInfo = value;
                OnPropertyChanged("UserPersonalInfo");
            }
        }

        #endregion

        #region ctor

        public ProfileViewModel()
        {
            SetUser();
        }

        private async void SetUser()
        {
            try
            {
                var user = await User.GetPersonalInfo();
                UserPersonalInfo = user;
                Console.WriteLine(UserPersonalInfo.Login);
            }
            catch (ArgumentNullException ex)
            {
                App.DisplayException(ex);
            }
            catch (HttpRequestException ex)
            {
                App.DisplayException(ex);
            }
            catch (DataFormatException ex)
            {
                App.DisplayException(ex);
            }
            catch (TaskCanceledException ex)
            {
                App.DisplayException(ex);
            }
            catch (SessionExpiredException ex)
            {
                App.DisplayException(ex);
            }
            catch (System.Exception ex)
            {
                App.DisplayException(ex);
            }
        }
        #endregion

        #region binding

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        #endregion
    }
}
