# OpenTelemetry Example netcore 5

Simple Weather Forecast API example using OpenTelemetry ASP.netcore basic instrumentation with jaeger exporter

## Installing & Running

Clone this repo

    git clone https://github.com/ppmaluch/opentelemetry-api-jaeger-example.git

Then run the docker images using the docker-compose file

    docker-compose up -d

## Seeing Results

Open your browser and go to [weatherforecast API](http://localhost:4500/weatherforecast) to see a response from the API

Then open the [Jaeger UI](http://localhost:16686/)

Look for the `Weather Service`

![Jaeger UI](/assets/jaegerui.png)

and hit `Find Traces`
