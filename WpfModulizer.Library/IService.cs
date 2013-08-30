namespace WpfModulizer.Library
{
    public interface IService
    {
        /// <summary>
        /// Остановить
        /// </summary>
        void Stop();

        /// <summary>
        /// Запустить
        /// </summary>
        void Run();

        /// <summary>
        /// Закрыть
        /// </summary>
        void Close();
    }
}