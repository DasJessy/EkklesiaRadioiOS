﻿namespace Firebase.Xamarin.Auth
{
    using System.ComponentModel;

    using Newtonsoft.Json;

    /// <summary>
    /// Basic information about the logged in user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the local id.
        /// </summary>
        [JsonProperty("localId", DefaultValueHandling = DefaultValueHandling.Populate)]
        [DefaultValue("")]
        public string LocalId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the federated id.
        /// </summary>
        [JsonProperty("federatedId", DefaultValueHandling = DefaultValueHandling.Populate)]
        [DefaultValue("")]
        public string FederatedId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [JsonProperty("firstName", DefaultValueHandling = DefaultValueHandling.Populate)] 
        [DefaultValue("")]
        public string FirstName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [JsonProperty("lastName", DefaultValueHandling = DefaultValueHandling.Populate)]
        [DefaultValue("")]
        public string LastName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        [JsonProperty("displayName", DefaultValueHandling = DefaultValueHandling.Populate)]
        [DefaultValue("")]
        public string DisplayName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [JsonProperty("email", DefaultValueHandling = DefaultValueHandling.Populate)]
        [DefaultValue("")]
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the photo url.
        /// </summary>
        [JsonProperty("photoUrl", DefaultValueHandling = DefaultValueHandling.Populate)]
        [DefaultValue("")]
        public string PhotoUrl
        {
            get;
            set;
        }
    }
}
