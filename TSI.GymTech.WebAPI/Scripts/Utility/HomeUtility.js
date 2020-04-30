
// Get URL link
function GetLinkUrl(apiAction) {
    var formAction = $("form").attr("action");
    var url;

    if (formAction.indexOf('Home/Index') >= 0) {
        url = formAction.substr(0, formAction.indexOf('Home/Index'));
    }

    else if (formAction.indexOf('Home/') >= 0) {
        url = formAction.substr(0, formAction.indexOf('Home/'));
    }

    else if (formAction.indexOf('Home') >= 0) {
        url = formAction.substr(0, formAction.indexOf('Home'));
    }

    else {
        url = formAction;
    }

    url += apiAction;
    return url.replace('//', '/');
}

// Update Student Total Analytics info
function BirthdaysOfTheDay() {
    var formAction = $("#logoutForm").attr("action");
    var urlBase = formAction.substr(0, formAction.indexOf('Account'));

    $.ajax({
        type: "GET",
        dataType: "json",
        url: urlBase + 'api/Person/GetBirthdaysOfTheDay',
        success: function (data) {
            if (data.Success) {
                // Applying or disabling the alert count
                if (data.BirthdaysOfTheDayCount > 0) {
                    $("#birthdays-of-the-day").text(data.BirthdaysOfTheDayCount + "+");
                }
                else {
                    $("#birthdays-of-the-day").addClass("d-none");
                }

                // Create the <a> element to each element from the list
                data.BirthdaysOfTheDayList.forEach(function (element) {
                    var profileType = element.ProfileType == 2 ? 'Student' : 'User';
                    var birthDayToday = element.BirthDay == 'Hoje'
                        ? '<span class="birthday-today">' + element.BirthDay + '</span>'
                        : '<span>' + element.BirthDay + '</span>';

                    $("#birthday-content").append(
                        '<a class="dropdown-item d-flex align-items-center" href="' + urlBase + profileType + '/Edit/' + element.Id +'">' +
                        '<div class="mr-3">' +
                            '<div class= "icon-circle bg-secondary" >' +
                                //'<i class="fas fa-file-alt text-white"></i>' +
                                '<img class="profile-picture img-profile rounded-circle" src="' + urlBase + 'Images/Persons/' + element.Photo +'">' +
                            '</div >' +
                        '</div >' +
                        '<div>' +
                            '<div class="small text-gray-900">' + 
                                    birthDayToday +
                                    element.BirthDate +
                            '</div>' +
                            '<span class="font-weight-bold">' + element.Name + '</span>' +
                        '</div>'
                    );
                });
            }
        }
    });
}

// Update Student Total Analytics info
function StudentTotalAnalyticsInfo() {
    var result = false;
         
    $.ajax({
        type: "GET",
        dataType: "json",
        url: GetLinkUrl('/api/Person/GetTotalOfStudents'),
        success: function (data) {
            if (data.Success) {
                result = true;
                $("#total-students").text(data.TotalStudents);
                $("#total-active-frequent-students").text(data.TotalActiveFrequentStudents);
                $("#total-active-not-frequent-students").text(data.TotalActiveNotFrequentStudents);
                $("#total-inactive-students").text(data.TotalInactiveStudents);
            }
        },
        complete: function () {
            if (!result) {
                $("#total-students").text(0);
                $("#total-active-students").text(0);
                $("#total-inactive-students").text(0);
            }
        }
    });
}

// Update Access Log Analytics info
function AccessLogAnalyticsInfo() {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: GetLinkUrl('/api/AccessLog/GetAccessLogGroupBy'),
        success: function (data) {
            if (data.Success) {
                UpdateAreaChart(data.Labels, data.Data);
            }
        }
    });
}

function UpdateAreaChart(labels, data) {
    // Area Chart Example
    var ctx = document.getElementById("myAreaChart");
    var myLineChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: "Qtd. Acesso",
                lineTension: 0.3,
                backgroundColor: "rgba(78, 115, 223, 0.05)",
                borderColor: "rgba(78, 115, 223, 1)",
                pointRadius: 3,
                pointBackgroundColor: "rgba(78, 115, 223, 1)",
                pointBorderColor: "rgba(78, 115, 223, 1)",
                pointHoverRadius: 3,
                pointHoverBackgroundColor: "rgba(78, 115, 223, 1)",
                pointHoverBorderColor: "rgba(78, 115, 223, 1)",
                pointHitRadius: 10,
                pointBorderWidth: 2,
                data: data,
            }],
        },
        options: {
            maintainAspectRatio: false,
            layout: {
                padding: {
                    left: 10,
                    right: 25,
                    top: 25,
                    bottom: 0
                }
            },
            scales: {
                xAxes: [{
                    time: {
                        unit: 'date'
                    },
                    gridLines: {
                        display: false,
                        drawBorder: false
                    },
                    ticks: {
                        maxTicksLimit: 7
                    }
                }],
                yAxes: [{
                    ticks: {
                        maxTicksLimit: 5,
                        padding: 10,
                        // Include a dollar sign in the ticks
                        callback: function (value, index, values) {
                            return number_format(value);
                        }
                    },
                    gridLines: {
                        color: "rgb(234, 236, 244)",
                        zeroLineColor: "rgb(234, 236, 244)",
                        drawBorder: false,
                        borderDash: [2],
                        zeroLineBorderDash: [2]
                    }
                }],
            },
            legend: {
                display: false
            },
            tooltips: {
                backgroundColor: "rgb(255,255,255)",
                bodyFontColor: "#858796",
                titleMarginBottom: 10,
                titleFontColor: '#6e707e',
                titleFontSize: 14,
                borderColor: '#dddfeb',
                borderWidth: 1,
                xPadding: 15,
                yPadding: 15,
                displayColors: false,
                intersect: false,
                mode: 'index',
                caretPadding: 10,
                callbacks: {
                    label: function (tooltipItem, chart) {
                        var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
                        return datasetLabel + ': ' + number_format(tooltipItem.yLabel);
                    }
                }
            }
        }
    });
}