﻿using Cinema.Services.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cinema.Services
{
    public class ConsumeScopedHostedService : IHostedService
    {
        private readonly IServiceProvider _service;
        public ConsumeScopedHostedService(IServiceProvider service)
        {
            _service = service;

        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await DoWork();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async Task DoWork()
        {
            using (var scope = _service.CreateScope())
            {
                var scopedProcessingService = scope.ServiceProvider.GetRequiredService<IBackGroundEmailSender>();
                await scopedProcessingService.DoWork();
            }
        }
    }
}
