import { useEffect, useRef } from 'react';
import 'ol/ol.css';
import Map from 'ol/Map';
import View from 'ol/View';
import { Tile as TileLayer } from 'ol/layer';
import { OSM } from 'ol/source';
import { Feature } from 'ol';
import { Point } from 'ol/geom';
import { Vector as VectorLayer } from 'ol/layer';
import { Vector as VectorSource } from 'ol/source';
import { Style, Icon } from 'ol/style';
import { useGeographic } from 'ol/proj';

const MapComponent = () => {
  const mapElement = useRef<HTMLDivElement | null>(null);
  const mapRef = useRef<Map | null>(null);
  const vectorSource = useRef(new VectorSource());

  const colonnine = [



    {
      "latitudine": 45.97160235135628,
      "longitudine": 12.615956840396523
    },
    {
      "latitudine": 46.00515756259164,
      "longitudine": 12.661798254933393
    },

    {
      "latitudine": 45.91427749277099,
      "longitudine": 12.694579786576838
    },

    {
      "latitudine": 45.99676270507915,
      "longitudine": 12.633694806262518
    },
    {
      "latitudine": 45.97716740338718,
      "longitudine": 12.70729099191655
    },



    {
      "latitudine": 45.93455553772048,
      "longitudine": 12.681788866972406
    },
    {
      "latitudine": 45.96944263015435,
      "longitudine": 12.634872014255915
    },

    {
      "latitudine": 45.98338040449174,
      "longitudine": 12.63359497068744
    },


    {
      "latitudine": 45.9777174109767,
      "longitudine": 12.618860069978858
    },
    {
      "latitudine": 45.97155897984618,
      "longitudine": 12.684092982562216
    },

  ]

  const pinStyle = new Style({
    image: new Icon({
      anchor: [0.5, 1],
      src: '../../public/img/charging.svg' // Inserisci l'URL dell'icona del pin
    })
  })

  const userLocationStyle = new Style({
    image: new Icon({
      anchor: [0.5, 1],
      src: '../../public/img/user.svg' // Inserisci l'URL dell'icona della posizione dell'utente
    })
  });


  useGeographic();

  useEffect(() => {
    if (mapElement.current) {
      const initialMap = new Map({
        target: mapElement.current,
        layers: [
          new TileLayer({
            source: new OSM()
          }),
          new VectorLayer({
            source: vectorSource.current,
            style: pinStyle
          })
        ],
        view: new View({
          zoom: 12
        })
      });

      mapRef.current = initialMap;

      if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition((position) => {

          const userLocation = [position.coords.longitude, position.coords.latitude];
          mapRef.current?.getView().setCenter(userLocation);

          const marker = new Feature({
            geometry: new Point(userLocation),
          });

          marker.setStyle(
            userLocationStyle
          );

          vectorSource.current.addFeature(marker);
        });
      } else {
        alert('Geolocation non è supportato dal tuo browser.');
      }
    }

    fetchPins();

    return () => {
      mapRef.current?.setTarget('');
    };
  }, []);

  const fetchPins = async () => {
    try {
      //const response = await axios.get('YOUR_API_ENDPOINT');
      const pins = colonnine; // Supponiamo che l'API restituisca un array di pin con latitudine e longitudine
      pins.forEach((pin: { latitudine: number; longitudine: number }) => {
        addMarker([pin.longitudine, pin.latitudine]);
      });
    } catch (error) {
      console.error('Errore durante il recupero delle coordinate:', error);
    }
  };

  const addMarker = (coordinate: number[]) => {
    const marker = new Feature({
      geometry: new Point(coordinate),

    });

    vectorSource.current.addFeature(marker);
  };

  return <div ref={mapElement} style={{ width: '100%', height: '100vh' }} />;
};

export default MapComponent;
