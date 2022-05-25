using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Abp;
using Abp.Dependency;
using Abp.Domain.Services;

namespace MauiBoilerplate.Core.ViewModel
{
    public abstract class ViewModelBase : AbpServiceBase, ISingletonDependency, INotifyPropertyChanged
    {
        public ViewModelBase()
        {
            LocalizationSourceName = MauiBoilerplateConsts.LocalizationSourceName;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected PropertyChangedEventHandler PropertyChangedHandler { get; }

        public void VerifyPropertyName(string propertyName)
        {
            Type type = GetType();
            if (!string.IsNullOrEmpty(propertyName) && type.GetTypeInfo().GetDeclaredProperty(propertyName) == null)
                throw new ArgumentException("找不到属性", propertyName);
        }

        public virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;
            if (propertyChanged == null)
                return;
            propertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            if (PropertyChanged == null)
                return;
            string propertyName = GetPropertyName(propertyExpression);
            if (string.IsNullOrEmpty(propertyName))
                return;
            RaisePropertyChanged(propertyName);
        }

        protected static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
                throw new ArgumentNullException(nameof(propertyExpression));
            MemberExpression body = propertyExpression.Body as MemberExpression;
            if (body == null)
                throw new ArgumentException("参数不合法", nameof(propertyExpression));
            PropertyInfo member = body.Member as PropertyInfo;
            if (member == null)
                throw new ArgumentException("找不到属性", nameof(propertyExpression));
            return member.Name;
        }

    }
}
