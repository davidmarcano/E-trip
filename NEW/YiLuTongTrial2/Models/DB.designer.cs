﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace YiLuTongTrial2.Models
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	public partial class DBDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertRoute(Route instance);
    partial void UpdateRoute(Route instance);
    partial void DeleteRoute(Route instance);
    partial void InsertRouteMember(RouteMember instance);
    partial void UpdateRouteMember(RouteMember instance);
    partial void DeleteRouteMember(RouteMember instance);
    #endregion

    public DBDataContext() :
        base(global::System.Configuration.ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString, mappingSource)
    {
        OnCreated();
    }
		public DBDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DBDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DBDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DBDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Route> Routes
		{
			get
			{
				return this.GetTable<Route>();
			}
		}
		
		public System.Data.Linq.Table<RouteMember> RouteMembers
		{
			get
			{
				return this.GetTable<RouteMember>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Route")]
	public partial class Route : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _RouteId;
		
		private string _RouteNumber;
		
		private System.DateTime _CreateTime;
		
		private string _UserId;
		
		private string _UserName;
		
		private string _UserPicture;
		
		private string _FromLongitude;
		
		private string _FormLatitude;
		
		private string _ToLongitude;
		
		private string _ToLatitude;
		
		private bool _IsOpen;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnRouteIdChanging(int value);
    partial void OnRouteIdChanged();
    partial void OnRouteNumberChanging(string value);
    partial void OnRouteNumberChanged();
    partial void OnCreateTimeChanging(System.DateTime value);
    partial void OnCreateTimeChanged();
    partial void OnUserIdChanging(string value);
    partial void OnUserIdChanged();
    partial void OnUserNameChanging(string value);
    partial void OnUserNameChanged();
    partial void OnUserPictureChanging(string value);
    partial void OnUserPictureChanged();
    partial void OnFromLongitudeChanging(string value);
    partial void OnFromLongitudeChanged();
    partial void OnFormLatitudeChanging(string value);
    partial void OnFormLatitudeChanged();
    partial void OnToLongitudeChanging(string value);
    partial void OnToLongitudeChanged();
    partial void OnToLatitudeChanging(string value);
    partial void OnToLatitudeChanged();
    partial void OnIsOpenChanging(bool value);
    partial void OnIsOpenChanged();
    #endregion
		
		public Route()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RouteId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int RouteId
		{
			get
			{
				return this._RouteId;
			}
			set
			{
				if ((this._RouteId != value))
				{
					this.OnRouteIdChanging(value);
					this.SendPropertyChanging();
					this._RouteId = value;
					this.SendPropertyChanged("RouteId");
					this.OnRouteIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RouteNumber", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string RouteNumber
		{
			get
			{
				return this._RouteNumber;
			}
			set
			{
				if ((this._RouteNumber != value))
				{
					this.OnRouteNumberChanging(value);
					this.SendPropertyChanging();
					this._RouteNumber = value;
					this.SendPropertyChanged("RouteNumber");
					this.OnRouteNumberChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreateTime", DbType="DateTime NOT NULL")]
		public System.DateTime CreateTime
		{
			get
			{
				return this._CreateTime;
			}
			set
			{
				if ((this._CreateTime != value))
				{
					this.OnCreateTimeChanging(value);
					this.SendPropertyChanging();
					this._CreateTime = value;
					this.SendPropertyChanged("CreateTime");
					this.OnCreateTimeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserId", DbType="VarChar(200) NOT NULL", CanBeNull=false)]
		public string UserId
		{
			get
			{
				return this._UserId;
			}
			set
			{
				if ((this._UserId != value))
				{
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._UserId = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserName", DbType="VarChar(200) NOT NULL", CanBeNull=false)]
		public string UserName
		{
			get
			{
				return this._UserName;
			}
			set
			{
				if ((this._UserName != value))
				{
					this.OnUserNameChanging(value);
					this.SendPropertyChanging();
					this._UserName = value;
					this.SendPropertyChanged("UserName");
					this.OnUserNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserPicture", DbType="VarChar(500)")]
		public string UserPicture
		{
			get
			{
				return this._UserPicture;
			}
			set
			{
				if ((this._UserPicture != value))
				{
					this.OnUserPictureChanging(value);
					this.SendPropertyChanging();
					this._UserPicture = value;
					this.SendPropertyChanged("UserPicture");
					this.OnUserPictureChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FromLongitude", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string FromLongitude
		{
			get
			{
				return this._FromLongitude;
			}
			set
			{
				if ((this._FromLongitude != value))
				{
					this.OnFromLongitudeChanging(value);
					this.SendPropertyChanging();
					this._FromLongitude = value;
					this.SendPropertyChanged("FromLongitude");
					this.OnFromLongitudeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FormLatitude", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string FormLatitude
		{
			get
			{
				return this._FormLatitude;
			}
			set
			{
				if ((this._FormLatitude != value))
				{
					this.OnFormLatitudeChanging(value);
					this.SendPropertyChanging();
					this._FormLatitude = value;
					this.SendPropertyChanged("FormLatitude");
					this.OnFormLatitudeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ToLongitude", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string ToLongitude
		{
			get
			{
				return this._ToLongitude;
			}
			set
			{
				if ((this._ToLongitude != value))
				{
					this.OnToLongitudeChanging(value);
					this.SendPropertyChanging();
					this._ToLongitude = value;
					this.SendPropertyChanged("ToLongitude");
					this.OnToLongitudeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ToLatitude", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string ToLatitude
		{
			get
			{
				return this._ToLatitude;
			}
			set
			{
				if ((this._ToLatitude != value))
				{
					this.OnToLatitudeChanging(value);
					this.SendPropertyChanging();
					this._ToLatitude = value;
					this.SendPropertyChanged("ToLatitude");
					this.OnToLatitudeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsOpen", DbType="Bit NOT NULL")]
		public bool IsOpen
		{
			get
			{
				return this._IsOpen;
			}
			set
			{
				if ((this._IsOpen != value))
				{
					this.OnIsOpenChanging(value);
					this.SendPropertyChanging();
					this._IsOpen = value;
					this.SendPropertyChanged("IsOpen");
					this.OnIsOpenChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.RouteMember")]
	public partial class RouteMember : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _UserId;
		
		private string _UserName;
		
		private string _UserPicture;
		
		private string _RouteNumber;
		
		private bool _OnRouting;
		
		private string _CurrentLongitude;
		
		private string _CurrentLatitude;
		
		private bool _IsHost;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnUserIdChanging(string value);
    partial void OnUserIdChanged();
    partial void OnUserNameChanging(string value);
    partial void OnUserNameChanged();
    partial void OnUserPictureChanging(string value);
    partial void OnUserPictureChanged();
    partial void OnRouteNumberChanging(string value);
    partial void OnRouteNumberChanged();
    partial void OnOnRoutingChanging(bool value);
    partial void OnOnRoutingChanged();
    partial void OnCurrentLongitudeChanging(string value);
    partial void OnCurrentLongitudeChanged();
    partial void OnCurrentLatitudeChanging(string value);
    partial void OnCurrentLatitudeChanged();
    partial void OnIsHostChanging(bool value);
    partial void OnIsHostChanged();
    #endregion
		
		public RouteMember()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserId", DbType="VarChar(200) NOT NULL", CanBeNull=false)]
		public string UserId
		{
			get
			{
				return this._UserId;
			}
			set
			{
				if ((this._UserId != value))
				{
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._UserId = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserName", DbType="VarChar(200) NOT NULL", CanBeNull=false)]
		public string UserName
		{
			get
			{
				return this._UserName;
			}
			set
			{
				if ((this._UserName != value))
				{
					this.OnUserNameChanging(value);
					this.SendPropertyChanging();
					this._UserName = value;
					this.SendPropertyChanged("UserName");
					this.OnUserNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserPicture", DbType="VarChar(200) NOT NULL", CanBeNull=false)]
		public string UserPicture
		{
			get
			{
				return this._UserPicture;
			}
			set
			{
				if ((this._UserPicture != value))
				{
					this.OnUserPictureChanging(value);
					this.SendPropertyChanging();
					this._UserPicture = value;
					this.SendPropertyChanged("UserPicture");
					this.OnUserPictureChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RouteNumber", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string RouteNumber
		{
			get
			{
				return this._RouteNumber;
			}
			set
			{
				if ((this._RouteNumber != value))
				{
					this.OnRouteNumberChanging(value);
					this.SendPropertyChanging();
					this._RouteNumber = value;
					this.SendPropertyChanged("RouteNumber");
					this.OnRouteNumberChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OnRouting", DbType="Bit NOT NULL")]
		public bool OnRouting
		{
			get
			{
				return this._OnRouting;
			}
			set
			{
				if ((this._OnRouting != value))
				{
					this.OnOnRoutingChanging(value);
					this.SendPropertyChanging();
					this._OnRouting = value;
					this.SendPropertyChanged("OnRouting");
					this.OnOnRoutingChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CurrentLongitude", DbType="VarChar(50)")]
		public string CurrentLongitude
		{
			get
			{
				return this._CurrentLongitude;
			}
			set
			{
				if ((this._CurrentLongitude != value))
				{
					this.OnCurrentLongitudeChanging(value);
					this.SendPropertyChanging();
					this._CurrentLongitude = value;
					this.SendPropertyChanged("CurrentLongitude");
					this.OnCurrentLongitudeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CurrentLatitude", DbType="VarChar(50)")]
		public string CurrentLatitude
		{
			get
			{
				return this._CurrentLatitude;
			}
			set
			{
				if ((this._CurrentLatitude != value))
				{
					this.OnCurrentLatitudeChanging(value);
					this.SendPropertyChanging();
					this._CurrentLatitude = value;
					this.SendPropertyChanged("CurrentLatitude");
					this.OnCurrentLatitudeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsHost", DbType="Bit NOT NULL")]
		public bool IsHost
		{
			get
			{
				return this._IsHost;
			}
			set
			{
				if ((this._IsHost != value))
				{
					this.OnIsHostChanging(value);
					this.SendPropertyChanging();
					this._IsHost = value;
					this.SendPropertyChanged("IsHost");
					this.OnIsHostChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
