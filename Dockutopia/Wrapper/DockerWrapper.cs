﻿using System;
using System.Diagnostics;
using Dockutopia.Model;

namespace Dockutopia.Wrapper
{
    public class DockerWrapper
    {
        private readonly Process _process;

        public event EventHandler<DataEventArgs> OutputDataReceived = (sender, args) => { };
        public event EventHandler<DataEventArgs> ErrorDataReceived = (sender, args) => { };
        public event EventHandler Exited = (sender, args) => { };

        public bool IsRunning { get; private set; }
        public bool HasExited { get; private set; }
        public int ProcessId { get; private set; }
        public int ExitCode { get; private set; }

        public DockerWrapper(string arguments = "")
        {
            var startInfo = new ProcessStartInfo()
            {
                //TODO check if docker is installed
                FileName = "docker",
                Arguments = arguments,
                // WorkingDirectory = workingDirectory,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
            };

            _process = new Process()
            {
                StartInfo = startInfo,
                EnableRaisingEvents = true,
            };
            _process.OutputDataReceived += DockerOutputDataReceived;
            _process.ErrorDataReceived += DockerErrorDataReceived;
            _process.Exited += DockerExited;
        }

        // Run command asynchronously...kind of
        public void BeginRun()
        {
            if (!IsRunning && !HasExited)
            {
                if (_process.Start())
                {
                    IsRunning = true;
                    ProcessId = _process.Id;

                    _process.BeginOutputReadLine();
                    _process.BeginErrorReadLine();
                }
            }
        }

        // Write command to standard input.
        public void WriteToStandardInput(string command)
        {
            if (IsRunning && !HasExited)
            {
                _process.StandardInput.Write(command);
            }
        }

        // Handler for OutputDataReceived event of process.
        private void DockerOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            OutputDataReceived(this, new DataEventArgs(e.Data));
        }

        // Handler for ErrorDataReceived event of process.
        private void DockerErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            ErrorDataReceived(this, new DataEventArgs(e.Data));
        }

        // Handler for Exited event of process.
        private void DockerExited(object sender, EventArgs e)
        {
            HasExited = true;
            IsRunning = false;
            ExitCode = _process.ExitCode;
            Exited(this, e);
        }
    }
}