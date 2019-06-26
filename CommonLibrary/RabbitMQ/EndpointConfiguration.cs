﻿using RabbitMQ.Client;
using System;

namespace Common.RabbitMQ
{
    public class EndpointConfiguration : IDisposable
    {
        public EndpointConfiguration(
            string uri = null,
            string initialName = null)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri(uri)
            };

            Model = factory.CreateConnection().CreateModel();

            AppDomain.CurrentDomain.ProcessExit += (sender, args) =>
                Dispose();
            Console.CancelKeyPress += (sender, args) =>
            {
                Dispose();
                args.Cancel = true;
            };

            InitialName = string.IsNullOrWhiteSpace(initialName)
                ? "NameNotSpecified."
                : initialName;
        }

        public IModel Model { get; }
        public string InitialName { get; }

        public void Dispose()
        {
            Model?.Close();
            Model?.Dispose();
        }
    }
}