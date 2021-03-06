﻿// Copyright Datalust Pty Ltd
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#if WINDOWS

using System; 
using System.IO;
using Seq.Forwarder.ServiceProcess;
using Seq.Forwarder.Util;

namespace Seq.Forwarder.Cli.Commands
{
    [Command("uninstall", "Uninstall the Windows service")]
    class UninstallCommand : Command
    {
        protected override int Run(TextWriter cout)
        {
            try
            {
                cout.WriteLine("Uninstalling service...");

                var sc = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "sc.exe");
                var exitCode = CaptiveProcess.Run(sc, $"delete \"{SeqForwarderWindowsService.WindowsServiceName}\"", cout.WriteLine, cout.WriteLine);
                if (exitCode != 0)
                    throw new InvalidOperationException($"The `sc.exe delete` call failed with exit code {exitCode}.");

                cout.WriteLine("Service uninstalled successfully.");
                return 0;
            }
            catch (Exception ex)
            {
                cout.WriteLine("Could not uninstall the service: " + ex.Message);
                return -1;
            }
        }
    }
}

#endif
