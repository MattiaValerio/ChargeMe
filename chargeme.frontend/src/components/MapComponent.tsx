import React, { useEffect, useRef } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'ol/ol.css';
import Map from 'ol/Map';
import View from 'ol/View';
import { Tile as TileLayer } from 'ol/layer';
import { OSM } from 'ol/source';
import { toLonLat } from 'ol/proj';
import { Feature } from 'ol';
import Select from 'ol/interaction/Select';
import { Point } from 'ol/geom';
import { Vector as VectorLayer } from 'ol/layer';
import { Vector as VectorSource } from 'ol/source';
import { Style, Icon } from 'ol/style';
import { useGeographic } from 'ol/proj';
import { click } from 'ol/events/condition';
import Overlay from 'ol/Overlay';

const MapComponent: React.FC = () => {
  const mapElement = useRef<HTMLDivElement | null>(null);
  const mapRef = useRef<Map | null>(null);
  const vectorSource = useRef(new VectorSource());
  const popupRef = useRef<Overlay | null>(null);
  let popoverInstance: any = null;

  const colonnine = [
    {
      "latitudine": 45.957775,
      "longitudine": 12.660396,
      "nome": "Mario",
      "cognome": "Rossi"
    },
    {
      "latitudine": 45.965000,
      "longitudine": 12.641111,
      "nome": "Luca",
      "cognome": "Bianchi"
    },
    {
      "latitudine": 45.972222,
      "longitudine": 12.635833,
      "nome": "Giovanni",
      "cognome": "Verdi"
    },
    {
      "latitudine": 45.980000,
      "longitudine": 12.649444,
      "nome": "Anna",
      "cognome": "Neri"
    },
    {
      "latitudine": 45.987222,
      "longitudine": 12.663056,
      "nome": "Sara",
      "cognome": "Gialli"
    },
    {
      "latitudine": 45.995000,
      "longitudine": 12.676667,
      "nome": "Paolo",
      "cognome": "Blu"
    },
    {
      "latitudine": 46.002222,
      "longitudine": 12.690278,
      "nome": "Lucia",
      "cognome": "Arancio"
    },
    {
      "latitudine": 46.009444,
      "longitudine": 12.703889,
      "nome": "Alessandro",
      "cognome": "Viola"
    },
    {
      "latitudine": 46.016667,
      "longitudine": 12.717500,
      "nome": "Elisa",
      "cognome": "Rosa"
    },
    {
      "latitudine": 46.024444,
      "longitudine": 12.731111,
      "nome": "Marco",
      "cognome": "Marrone"
    }
  ];

  const pinStyle = new Style({
    image: new Icon({
      anchor: [0.5, 1],
      src: '../../public/img/charging.svg',
      scale: 1,
      size: [64, 64],
    }),
  });

  const userLocationStyle = new Style({
    image: new Icon({
      anchor: [0.5, 1],
      src: '../../public/img/user.svg',
      scale: 1,
      size: [64, 64],
    }),
  });

  useGeographic();

  useEffect(() => {
    if (mapElement.current) {
      const initialMap = new Map({
        target: mapElement.current,
        layers: [
          new TileLayer({
            source: new OSM(),
          }),
          new VectorLayer({
            source: vectorSource.current,
            style: pinStyle,
          }),
        ],
        view: new View({
          zoom: 12,
        }),
      });

      mapRef.current = initialMap;

      if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition((position) => {
          const userLocation = [position.coords.longitude, position.coords.latitude];
          mapRef.current?.getView().setCenter(userLocation);

          const marker = new Feature({
            geometry: new Point(userLocation),
          });

          marker.setStyle(userLocationStyle);
          vectorSource.current.addFeature(marker);
        });
      } else {
        alert('Geolocation non è supportato dal tuo browser.');
      }

      const selectClick = new Select({
        condition: click,
        hitTolerance: 10,
        style: null,
      });

      initialMap.addInteraction(selectClick);

      selectClick.on('select', (e) => {
        if (e.selected.length > 0) {
          const feature = e.selected[0];
          const properties = feature.getProperties();
          const geometry = feature.getGeometry();
          if (geometry instanceof Point) {
            const coordinates = geometry.getCoordinates();
            //const hdms = toLonLat(coordinates);
            const popup = new Overlay({
              element: document.getElementById('popup')!,
            });
            if (popupRef.current) {
              initialMap.removeOverlay(popupRef.current);
            }
            popupRef.current = popup;
            initialMap.addOverlay(popup);
            popup.setPosition(coordinates);

            const element = popup.getElement();
            if(element){
              element.innerHTML = `<div class="popover bs-popover-top">
              <div class="arrow"></div>
              <h3 class="popover-header">Marker Info</h3>
              <div class="popover-body">
                <p>Nome: ${properties.nome}</p>
                <p>Cognome: ${properties.cognome}</p>
              </div>
            </div>`;
            }
            
            
            if (popoverInstance) {
              popoverInstance.dispose();
            }
            popoverInstance = new (window as any).bootstrap.Popover(element, {
              animation: false,
              container: element,
              html: true,
              placement: 'top',
            });
            popoverInstance.show();
          }
        }
      });

      // Listener globale per chiudere il popup quando si clicca altrove
      initialMap.on('click', (event) => {
        if (!initialMap.hasFeatureAtPixel(event.pixel)) {
          if (popoverInstance) {
            popoverInstance.dispose();
            popoverInstance = null;
          }
          if (popupRef.current) {
            popupRef.current.setPosition(undefined);
          }
        }
      });

      fetchPins();

      return () => {
        mapRef.current?.setTarget('');
      };
    }
  }, []);

  const fetchPins = async () => {
    try {
      const pins = colonnine;
      pins.forEach((pin: { latitudine: number; longitudine: number; nome: string; cognome: string }) => {
        addMarker([pin.longitudine, pin.latitudine], pin.nome, pin.cognome);
      });
    } catch (error) {
      console.error('Errore durante il recupero delle coordinate:', error);
    }
  };

  const addMarker = (coordinate: number[], nome: string, cognome: string) => {
    const marker = new Feature({
      geometry: new Point(coordinate),
      nome: nome,
      cognome: cognome,
    });
    vectorSource.current.addFeature(marker);
  };

  return (
    <div style={{ width: '100%', height: '100vh', position: 'relative' }}>
      <div ref={mapElement} style={{ width: '100%', height: '100vh' }} />
      <div id="popup" className="ol-popup"></div>
    </div>
  );
};

export default MapComponent;
