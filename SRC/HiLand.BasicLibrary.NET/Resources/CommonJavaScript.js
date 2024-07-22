function format(string) {
    var args = arguments;
    var pattern = new RegExp("{([0-" + arguments.length + "])}", "g");
    return String(string).replace(pattern, function (match, index) {
        var currentIndex = parseInt(index);
        if (currentIndex + 1 > args.length || currentIndex < 0) {
            throw new Error("error args");
        }
        return args[currentIndex + 1];
    });
};