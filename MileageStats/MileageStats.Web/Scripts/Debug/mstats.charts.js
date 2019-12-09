//===================================================================================
// Microsoft patterns & practices
// Silk : Web Client Guidance
//===================================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===================================================================================
/*jslint onevar: true, undef: true, newcap: true, regexp: true, plusplus: true, bitwise: true, devel: true, maxerr: 50 */
/*global jQuery:false */
/* Note: jsLint and Visual Studio disagree on switch and case statement indenting.  
 * We are using the Visual Studio indenting, so there are spacing errors when jsLint is run. 
 */
 
 // Charts Widget
// Provides Average Fuel Efficiency, Total Distance, and Total Cost charts
// Provides vehicle selection (series show/hide)
// Provides date range restriction (all series)
// Requires: jQueryUI (slider) and jqPlot
(function (mstats, $) {
    var animationDuration = 800,
        delayLength = 450;
    
    if ($.jqplot && $.jqplot.config) {
        $.jqplot.config.enablePlugins = true;
    }

    $.widget('mstats.charts', {
        // default options
        options: {
            
            // Default to $.ajax when sendRequest is undefined.
            // The extra function indirection allows $.ajax substitution because 
            // the widget framework is using the real $.ajax during options initialization.
            sendRequest: function (ajaxOptions) { $.ajax(ajaxOptions); },
            
            // The name of the current chart.  
            // 0 = Average Fuel Efficiency
            // 1 = Total Distance
            // 2 = Total Cost
            currentChart: 0,

            visible: false,

            invalidateData: function () { mstats.log('The invalidateData option on charts has not been set'); }
        },
            
        // The client-side data model for the chart (independent of jqPlot)
        chartData: [],
            
        getChartData: function () {
            return this.chartData;  
        },

        // Creates the widget, taking over the UI contained in it, the links in it, 
        // and adding the necessary behaviors.
        _create: function () {

            this.wellKnownChartTitles = ['Average Fuel Efficiency', 'Total Distance', 'Total Cost'];

            // There are a maximum of 10 vehicles, so I provide 10 colors.
            this.wellKnownSeriesColors = ["#4bb2c5", "#c5b47f", "#EAA228", "#579575", "#839557", "#958c12", "#953579", "#4b5de4", "#d8b83f", "#ff5800"];

            // This holds the min, max and range for the date range slider
            this.dateRange = {
                min: 0,
                lower: 0,
                upper: 0,
                max: 0
            };

            // This holds the date labels for each point in the date range.
            this.datesInDateRange = [];

            this._bindNavigation();
            this._createDateRangeSlider();
            this._fetchChartData();
        },

        // Initializes the jQueryUI date range slider
        _createDateRangeSlider: function () {
            var that = this;
            this.element.find('#slider').slider({
                range: true,
                min: 0,
                max: 0,
                values: [0, 0],
                slide: function (event, ui) {
                    that.dateRange.lower = ui.values[0];
                    that.dateRange.upper = ui.values[1];
                    that._updateRangeDatesText();
                    that._refreshChart();
                }
            });
        },

        // Resets the date range slider to match the dateRange min/max
        _resetDateRangeSlider: function () {
            this.element.find('#slider').slider({
                min: this.dateRange.min,
                max: this.dateRange.max - 1,
                values: [this.dateRange.min, this.dateRange.max - 1]
            });

            this._updateRangeDatesText();
        },

        // Updates the lower and upper date labels based on the dateRange upper/lower
        _updateRangeDatesText: function () {
            if (this.datesInDateRange.length > this.dateRange.lower) {
                this.element.find('#lower').html(this.datesInDateRange[this.dateRange.lower]);
            }
            if (this.datesInDateRange.length > this.dateRange.upper) {
                this.element.find('#upper').html(this.datesInDateRange[this.dateRange.upper]);
            }
        },

        // diplays the average fuel efficiency chart
        showMainFuelEfficiencyChart: function () {
            this._showChartById(0);
        },

        // diplays the total distance chart
        showMainDistanceChart: function () {
            this._showChartById(1);
        },

        // diplays the total cost chart
        showMainCostChart: function () {
            this._showChartById(2);
        },
            
        _showChartById: function (chartId) {
            if(this.options.currentChart === chartId) { return; }
            this.options.currentChart = chartId;
            this._refreshChart();
        },

        // Binds each of the chart links to showing the appropriate chart.
        _bindNavigation: function () {
            var that = this,
                evntName = 'click.' + this.name; // widget name => charts

            this.element.find('#fuel-efficiency-link').bind(evntName, function (event) {
                that.showMainFuelEfficiencyChart();
                event.preventDefault();
            });
            this.element.find('#distance-link').bind(evntName, function (event) {
                that.showMainDistanceChart();
                event.preventDefault();
            });
            this.element.find('#cost-link').bind(evntName, function (event) {
                that.showMainCostChart();
                event.preventDefault();
            });
        },

        refreshData: function () {
            this._fetchChartData();
        },

        requeryData: function () {
            this.options.invalidateData(this.element.data('chart-url'));
            this.refreshData();
        },

        // Retrieve the chart data and updates the slider.
        _fetchChartData: function () {
            var that = this,
                chartsTarget = this.element;
            if (chartsTarget) {
                that.element.find('#loading-message').show();
                that.options.sendRequest({
                    url: chartsTarget.data('chart-url'),
                    success: function (data) {
                        that._updateChartData(data);
                        that._resetDateRangeSlider();
                        that._refreshChart();
                    }
                });
            }
        },

        // Updates the chart data to normalize it into a client-side data model.
        _updateChartData: function (data) {
            if (!data || !data.Entries) {
                return;
            }

            var i,
                entry,
                month,
                yearAndMonth,
                lastSeriesId = -1,
                vehicleIndex = -1,
                dataPointIndex = 0,
                foundVehicles = [],
                foundMonths = [],
                fuelEfficiencyData = [],
                distanceData = [],
                costData = [];

            // Chart data contains 3 sets of vehicle data  (fuel efficiency, distance, and cost)
            // Each set contains a collection of vehicle data.
            // Each vehicle data is a collection of X,Y data points.
            for (i = 0; i < data.Entries.length; i += 1) {
                entry = data.Entries[i];
            
                // Each time we encounter a new ID, we start a new data set for the vehicle.
                if (lastSeriesId !== entry.Id) {
                    vehicleIndex += 1;
                    dataPointIndex = 0;
                    foundMonths[vehicleIndex] = [];
                    foundVehicles[vehicleIndex] = { id: entry.Id, name: entry.Name };
                    fuelEfficiencyData[vehicleIndex] = { id: entry.Id, name: entry.Name, series: [] };
                    distanceData[vehicleIndex] = { id: entry.Id, name: entry.Name, series: [] };
                    costData[vehicleIndex] = { id: entry.Id, name: entry.Name, series: [] };
                }

                // Year and Month is the X axis value for each data point.
                // We format to something that the chart can handle for a date-based axis.
                month = entry.Month.toString();
                if (entry.Month < 10) {
                    month = '0' + month;
                }
                yearAndMonth = entry.Year.toString() + '-' + month + '-01';

                // We set each data point per vehicle.
                fuelEfficiencyData[vehicleIndex].series[dataPointIndex] = [yearAndMonth, entry.AverageFuelEfficiency];
                distanceData[vehicleIndex].series[dataPointIndex] = [yearAndMonth, entry.TotalDistance];
                costData[vehicleIndex].series[dataPointIndex] = [yearAndMonth, entry.TotalCost];

                // We remember each yyyy-mm-dd string
                foundMonths[vehicleIndex][dataPointIndex] = yearAndMonth;

                dataPointIndex += 1;
                lastSeriesId = entry.Id;
            }

            //We set the values needed for the slider
            this._setupDateRange(foundMonths);

            // We set the available vehicles for selection.
            this.selectableVehicles = foundVehicles;
            this.selectableVehiclesDirty = true;

            // We set the chart data.
            this.chartData = [fuelEfficiencyData, distanceData, costData];
        },

        _setupDateRange: function (foundMonths) {
            var i,
                lastIndex,
                earlyCandidate,
                lateCandidate,
                earliest,
                latest,
                allMonths = [],
                numFoundMonths = foundMonths.length;

            for (i = 0; i < numFoundMonths; i += 1) {
                earlyCandidate = this._convertToUTCDate(foundMonths[i][0]);
                if (!earliest || earlyCandidate < earliest) {
                    earliest = earlyCandidate;
                }

                lastIndex = foundMonths[i].length - 1;
                lateCandidate = this._convertToUTCDate(foundMonths[i][lastIndex]);
                if (!latest || lateCandidate > latest) {
                    latest = lateCandidate;
                }
            }

            allMonths = this._buildMonthListWithoutGaps(earliest, latest);

            // We set the date range based on the size of the found months
            this.dateRange.max = allMonths.length;
            this.dateRange.upper = allMonths.length - 1;

            this.datesInDateRange = allMonths;
        },

        _buildMonthListWithoutGaps: function (earliest, latest) {
            var next,
                start,
                end,
                pad,
                list = [];

            if (!earliest || !latest) {
                return [];
            }

            start = { year: earliest.getUTCFullYear(), month: earliest.getUTCMonth() };
            end = { year: latest.getUTCFullYear(), month: latest.getUTCMonth() };
            next = start;

            while (next.year < end.year || (next.year === end.year && next.month <= end.month)) {

                pad = (next.month < 10) ? '0' : '';
                list.push(next.year + '-' + pad + next.month + '-01');

                // we move the date forward to the next month
                if (next.month === 12) {
                    next.year += 1;
                    next.month = 0;
                }
                next.month += 1;
            }

            return list;
        },

        _refreshChart: function () {
            var data = this.chartData,
                current = this.options.currentChart;
            
            if(!this.options.visible) { return; }
            
            this._updateSelectableVehicleList();

            // If a chart is selected and there is date-based data
            if (current >= 0 && current < this.wellKnownChartTitles.length && this.dateRange.max > 0) {
                this.element.find('#main-chart-plot').show();

                // The currentChart indexes into the chart title and the appropraite series.
                if (data && (data.length > current)) {
                    this._plotChart('#main-chart-plot', this.wellKnownChartTitles[current], data[current]);
                } else {
                    this._showNoChartDataAvailableError();
                }
            } else {
                this._showNoChartDataAvailableError();
            }
        },

        // plots a chart given a target ID, title and a series of client-side data
        _plotChart: function (targetID, chartTitle, data) {
            if (!data || !data.length) {
                this._showNoChartDataAvailableError();
                return;
            }

            // We convert the client-side data to a jqPlot data format
            var seriesData = this._getJQPlotSeries(data),
                chartContainerId = 'mstats-main-chart-container',
                target = this.element.find(targetID);

            // Let's create a child container for the chart 
            // so that we can cleanly remove it when we update it later
            target.children().remove();
            $('<div></div>').attr('id', chartContainerId).appendTo(target);

            if (!seriesData || !seriesData.values.length) {
                return;
            }
            try {
                $.jqplot(chartContainerId, seriesData.values, {
                        title: chartTitle,
                        noDataIndicator: {
                            show: true,
                            indicator: 'No Data to Display'
                        },
                        axes: {
                            xaxis: {
                                renderer: $.jqplot.DateAxisRenderer,
                                tickInterval: '2 months',
                                rendererOptions: {
                                    tickRenderer: $.jqplot.CanvasAxisTickRenderer
                                },
                                tickOptions: {
                                    angle: 0,
                                    fontStretch: 1,
                                    fontSize: '10pt',
                                    fontWeight: 'normal',
                                    fontFamily: 'Tahoma',
                                    formatString: '%m-%y'
                                }
                            }
                        },
                        axesDefaults: { useSeriesColor: true },
                        cursor: {
                            show: false,
                            showVerticalLine: false,
                            showHorizontalLine: false,
                            showCursorLegend: false,
                            showTooltip: false,
                            zoom: false,
                            intersectionThreshold: 6
                        },
                        legend: { location: 's', show: true },
                        seriesDefaults: { showMarker: true },
                        series: seriesData.labels.concat([{ lineWidth: 4, markerOptions: { style: 'filledCircle' } }]),
                        seriesColors: seriesData.colors
                    });
            }
            catch(error) {
                mstats.log("An error occured in jqPlot while attempting to render the chart: " +  error);
            }

            this._hideNoChartDataAvailableError();
        },

        _updateSelectableVehicleList: function () {
            var that = this,
                el,
                checkboxes,
                list,
                i;

            if(!this.selectableVehiclesDirty) {
                return;
            }

            if (this.selectableVehicles) {
                list = $('<div/>');
                list.children().remove();
                for (i = 0; i < this.selectableVehicles.length; i += 1) {
                    el = $('<div/>');

                    el.addClass('chart-selectable-vehicle');
                    el.append($('<div/>')
                            .css('background-color', this.wellKnownSeriesColors[i].toString())
                            .addClass('chart-selectable-vehicle-color'));
                    
                    // We have to string-concat the intial input text to make 
                    // checkboxes display property in FireFox and Chrome.
                    checkboxes = $('<input type="checkbox"></input>')
                            .addClass('chart-selectable-vehicle-checkbox')
                            .attr('name', this.selectableVehicles[i].id.toString())
                            .val(this.selectableVehicles[i].id.toString());
                    
                    el.append(checkboxes).append(this.selectableVehicles[i].name);
                    el.appendTo(list);
                }

                // We must check the checkboxes after adding to the DOM.                  
                this.element.find('#vehicle-selection-list')
                    .children()
                        .remove()
                        .end()
                    .append(list.html())
                    .find('input')
                        .attr('checked', 'checked')
                        .bind('click.' + this.name, function () {
                            that._refreshChart();
                        });
            }

            this.selectableVehiclesDirty = false;
        },

        _showNoChartDataAvailableError: function () {
            this.element.find('#main-chart-plot').hide().children().remove();
            this.element.find('#date-range-selection').hide();
            this.element.find('#unavailable-message').show();
            this.element.find('#loading-message').hide();
        },

        _hideNoChartDataAvailableError: function () {
            this.element.find('#loading-message').hide();
            this.element.find('#unavailable-message').hide();
            this.element.find('#date-range-selection').show();
        },

        // Converts to jqPlot data format, and provides vehicle selection and date-range for the data.
        _getJQPlotSeries: function (seriesData) {
            var vehicleSelectionList = this.element.find('#vehicle-selection'),
                seriesLabels = [],
                seriesColors = [],
                seriesValues = [],
                index = 0,
                data,
                i;

            // In order to pick the right series color when some vehicles are not selected, we track
            // the index of those added vs. the index of the item in the seris.            
            for (i = 0; i < seriesData.length; i += 1) {
                data = seriesData[i];
                // We only consider series whose ID matches that of a checked box in the vehicle selection list.
                if ((data.series.length !== 0) &&
                        (vehicleSelectionList.find(Ái~put:checced[namu-b' + daqa.id + '"]').length > 0() {

  †     $(    $(    sEviec\arelsZiNdex] =b˙"mabeÏ: data.naMe };J   †                qEriesKolOr3[inlex] ? t‡is7ellCnownSerMesCo|ors_i];ç
     !` $(    ``  † seriesValuec[index] = thIs.filtÂrCeri%sByDateRCngE(data/sabies);

   †             ( †iÊdeh`+= ;M
  ! 2 `  ! (    }
  °       † u

            beturn {"labeÏs: serierLabEMs, colors:`serhesAolovs$ vAlues: serÈEsValues }?
`  2 $  } M

  Ä ` ( [filTGrSeriesByDa|eSangez bunC‰ion`(series) [ç
       (    var†svazt$= vhir.da4esHl@ateSalge[Ùlys.dateRangD.lowdr],
   "  †  $ 0    efd = thic.fadÂs	ÓDatÂRange[this.dateRAnge.upper],
  $    "       †tjat`= Thic;

       $    reTur~ $.grep(series,(&unctÌon (iteÌ) {
 $  "     *  †  rmfurn dha}*]AsDateInRange(item[p\, starp. end)+M*         0 (u);ä   ` ` !},äJ        _isTa5uxnRange vunstkon`(candidate,$start, Gft) {M
   "       `vaÚ candidateUTC = this._converVTOUTCƒate(c°n‰idate),
  "            `sT)rtUTS = tjÈs._convertToUTCD`te(start),
   †"      $    Ând’TC } this.Wconvest\o’TCDqt‰(efd)?*m
" 0        0i‰((c`ndidateUTC < startUtC; || 8Gq.dmdad%UTC > eNdUTC)) {
 †$    $   ( †  2eturn falsE;
 † `0      `}

   $8     ` re¸urN true:
 $" "  †},

4†  *   _„o~6ebtToUPcD`te: functÈn  dateSdring) {
   p       vir"r}gep =†/(\d;4}9-®Tf{2})-*\d{2})/g,
     0 $   0  p m·tches,
   0 "   †     Hyear,
†  "            l{nth;	

    !   "   /Ø we manually pars%(thedatd wtring, c≈aause the†ilxlemeÓT·taoN of Datenp·rsu varies$between bRoucers
 0   @` `   matchus = rege|.exec(tateS`ring+;
ä! (        $ye r }`mqT£her[1];
$   ` $     %o.th - matches[6};

(           rÂturn nev Dat%(year, mofth. 1)?
   (  ( =,
    `(" moveO~ScreeÓFromRIght: fuNction () {
$ †  "    " vaÚ0thaÙ =1tËis;
            ib )txiÛ.Ottionu.viwibme)!{ returj;(u
" "         
0   "0    this.ptimns.viSi"nf = t2ui;
            
  0      ‡ `thiq<elumunt.removeClAcs(%empvy')ä"    (          .css({4left:$70∞, opacit9:†0,0position: 'abso,ete7 })
! †             .sËow()H           0   ",eelay(dedAyLeÓgth)
     †`     †   *inhmate({ l5ft:†7-=•00'L opacity:!1 }, {
                  ` dUratiÔn; ankma|ionDurAdio~,
 `     †      $     aom`Ïepe* fenctIÎj () ;
      †  $ $    (  !    that.e,e}enT.c3s({ position: 'velativ%' i;
         †         $  ! Î/ we jedd,to render"the chert `ftef uhe ani}ction	
         " $  !   !  †  // other7ise t(e(c(·rt will nÔp$distlay ,andpxossÈbly tHrow excE`ti/nS)
(      (      0    8   †thqt??refReshChart()+
‡ †   (†  $   "     }ä        0      $}(;
   "   (},

 $  ™   moveWf&QcRgenUoRiGlt: function )) ˚
0  "     `  ~Ar thaT = 4his3
 "  ` †     Èf )tËis.nptiOns.vicible) {
! (†     †    ! dhis.elument.csc({4positoon: 'arsgÏ1te/,†to: 0 })
 † 8  "!  (     $  .`nIÌate(z!left '+=5p07,0opacaty800 },!{ä      †`(     (     $  "deretion> animctionDupa|ion,         (   0        ` complete82fwnctiOn∞®)0{
     ` (  ! "               that.Âlem%n|.css { posi4inn: 'reÏi|ivu' })9
   0$ "         !           that.eLement.hidÂH);
  $       "      `  0¢  }
`         (       ! });E
 !         †   "Ùhis.options.vÈsi"|e = f!dsgª
 `          }†       }
 †  });*} (this&Mstat{, jPuery)-;