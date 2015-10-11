class LogService
{
    constructor($http)
    {
        this.$http = $http;
    }

    static logFactory($http) {
        return new LogService($http);
    }

    getAllSettings() {
        return this.$http.get('https://localhost:44300/logger/settings').then(result => result.data);
    }

    getActiveBooks(){
        return this.$http.get('/api/activeBooks').then(result => result.data );
    }

    getArchivedBooks(){
        return this.$http.get('/api/archivedBooks').then(result => result.data );
    }

    markBookRead(bookId, isBookRead){
        return this.$http.put(`/api/markRead/${bookId}`, {bookId: bookId, read: isBookRead});
    }

    addToArchive(bookId){
        return this.$http.put(`/api/addToArchive/${bookId}`,{});
    }

    checkIfBookExists(title){
        return this.$http.get(`/api/bookExists/${title}`).then(result =>  result.data );
    }

    addBook(book){
        return this.$http.post('/api/books', book);
    }
}

LogService.logFactory.$inject = ['$http'];

angular.module(moduleName)
  .factory('logService', LogService.logFactory);