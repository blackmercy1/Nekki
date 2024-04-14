public interface ITypeStat<T> : IStat<T>
{
    //в качестве id была выбрана string, да она медлеенее, но зато дебажить ее намного проще
    string Id();
}