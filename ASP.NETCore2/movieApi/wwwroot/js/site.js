$(document).ready(function () {
    getMovies();
})

$(document).on("submit", ".movieForm", function (e) {
    e.preventDefault();
    var movieName = $('.movieForm').serializeArray()
    var movieString = movieName[0].value;
    var url = "https://api.themoviedb.org/3/search/movie?api_key=43a8bc2209bf668d737b255b5a69b54c&query=" + movieName[0].value;
    $.get(url, function (ApiResponse) {
        var movieObj = {
            "movie": [ApiResponse["results"][0]["title"], ApiResponse["results"][0]["release_date"], ApiResponse["results"][0]["vote_average"].toString()]
        };
        console.log(movieObj);
        $.ajax({
            url: '/addMovie',
            type: 'POST',
            data: movieObj,
            success: function (data) {
                displayMovies(data);
            }
        });
    })
})

function findMovies(movie) {
    var movieInfo = $('form').serializeArray();
}

function getMovies(movie) {
    $("#notes").text("");
    $.get("/getMovies", function (movies) {
        for (movie of movies) {
            var movieArr = [];
            movieArr.push(movie["movie"], movie["release_date"].toString(), movie["rating"]);
            displayMovies(movieArr);
        }
    })
}

function displayMovies(movie) {
    $("#movieBox").prepend(`
        <tr>
            <td>${movie[0]}</td>
            <td class="bold">${movie[2]}</td>
            <td class="bold">${movie[1].toString()}</td>
        </tr>
    `);
}