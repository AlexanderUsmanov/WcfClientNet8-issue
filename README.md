# WcfClientNet8-issue
This repository contains repro of issue in managed ntlm over net-tcp wcf connection

To confirm this issue please do following:
1. Build solution
2. Run WcfServiceNet472 on windows host
3. Run WcfClientNet8 on ubuntu 20.04 (because there is known issue with OpenSSL 3 on ubuntu 22.04) in wsl or on dedicated host
4. Use ip address and credentials of windows host 
5. In case of UseManagedNtlm is set - thrown error. Otherwise - everything works.