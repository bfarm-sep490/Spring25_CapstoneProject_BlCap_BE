using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Helper
{
    public class FCMHelper
    {
        public FCMHelper()
        {
            var firebaseConfig = Environment.GetEnvironmentVariable("FIREBASE_CONFIG");
            if (string.IsNullOrEmpty(firebaseConfig))
            {
                throw new Exception("Firebase configuration is missing...");
            }

            var credential = GoogleCredential.FromJson(firebaseConfig);
            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = credential
                });
            }
        }

        public async Task<string> SendMessageToDevice(string title, string body, string dvToken)
        {
            var firebaseMessage = new Message()
            {
                Token = dvToken,
                Notification = new Notification()
                {
                    Title = title,
                    Body = body
                }
            };

            string response = await FirebaseMessaging.DefaultInstance.SendAsync(firebaseMessage);
            return response;
        }

        public async Task<string> SendMessageWithTopic(string title, string body, string topic)
        {
            var message = new Message()
            {
                Topic = topic,
                Notification = new Notification()
                {
                    Title = title,
                    Body = body
                }
            };

            string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            return response;
        }
    }
}
