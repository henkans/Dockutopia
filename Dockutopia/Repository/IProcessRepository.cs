namespace Dockutopia.Repository
{
    interface IProcessRepository
    {
        void BeginRun();
        void WriteToStandardInput(string command);
    }
}
