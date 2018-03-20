using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;

namespace SandboxerDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            PermissionSet pset = new PermissionSet(PermissionState.None);
            pset.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));

            // create the sandboxed domain
            AppDomain sandbox = AppDomain.CreateDomain(
                "Sandboxed Domain",
                AppDomain.CurrentDomain.Evidence,
                new AppDomainSetup(),
                pset,
                CreateStrongName(Assembly.GetExecutingAssembly()));

            sandbox.ExecuteAssembly("SandboxerDemo.exe");
        }
        public static StrongName CreateStrongName(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            AssemblyName assemblyName = assembly.GetName();
            Debug.Assert(assemblyName != null, "Could not get assembly name");

            // get the public key blob
            byte[] publicKey = assemblyName.GetPublicKey();
            if (publicKey == null || publicKey.Length == 0)
                throw new InvalidOperationException("Assembly is not strongly named");

            StrongNamePublicKeyBlob keyBlob = new StrongNamePublicKeyBlob(publicKey);

            // and create the StrongName
            return new StrongName(keyBlob, assemblyName.Name, assemblyName.Version);
        }
    }
}
