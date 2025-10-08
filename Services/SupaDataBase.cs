using Supabase;
using Geocampus.Map.API.Models;
using GeoCampus.Models;

namespace GeoCampus.Map.API.Services;

public class SupaDataBase(Client supa)
{
    private readonly Client Supabase = supa;


    public (double lon, double lat) ToLonLat(double x, double y)
    {
        double d = Math.Exp((0.0 - y) / 6378137.0);
        double num = Math.PI / 2.0 - 2.0 * Math.Atan(d);
        double item = x / 6378137.0 / (Math.PI / 180.0);
        double item2 = num / (Math.PI / 180.0);

        return (lon: item, lat: item2);
    }

    private GeoPosition GetPointOfInterest(PositionGeo posGeo, List<PhotoDestination> photos, List<Salle> salles, List<Infrastructure> infras, List<Surface> surfaces)
    {
        var salle = salles.FirstOrDefault(s => s.IdPositionGeo == posGeo.Id);
        if (salle != null)
        {

            (double lon, double lat) position = ToLonLat(posGeo.Longitude, posGeo.Latitude);

            posGeo.Longitude = position.lon;
            posGeo.Latitude = position.lat;

            return new GeoPosition(posGeo.Id, posGeo.Latitude, posGeo.Longitude, salle.Nom, salle.Description, photos.FirstOrDefault(p => p.Id == salle.IdPhotoDestination)?.CheminVersDestination ?? "");
        }

        var infra = infras.FirstOrDefault(i => i.IdPositionGeo == posGeo.Id);
        if (infra != null)
        {
            (double lon, double lat) position = ToLonLat(posGeo.Longitude, posGeo.Latitude);

            posGeo.Longitude = position.lon;
            posGeo.Latitude = position.lat;

            return new GeoPosition(posGeo.Id, posGeo.Latitude, posGeo.Longitude, infra.Nom, infra.Description, photos.FirstOrDefault(p => p.Id == infra.IdPhotoDestination)?.CheminVersDestination ?? "");
        }

        var surface = surfaces.FirstOrDefault(su => su.IdPositionGeo == posGeo.Id);
        if (surface != null)
        {
            (double lon, double lat) position = ToLonLat(posGeo.Longitude, posGeo.Latitude);

            posGeo.Longitude = position.lon;
            posGeo.Latitude = position.lat;

            return new GeoPosition(posGeo.Id, posGeo.Latitude, posGeo.Longitude, surface.Nom, surface.Description, photos.FirstOrDefault(p => p.Id == surface.IdPhotoDestination)?.CheminVersDestination ?? "");
        }
            
        return new GeoPosition(posGeo.Id, 0, 0);
    }

    public async Task<List<GeoPosition>> GetPointsOfInterests()
    {
        try
        {
            var pg = await Supabase
                .From<PositionGeo>()
                .Get();

            var ph = await Supabase
                .From<PhotoDestination>()
                .Get();

            var sl = await Supabase
                .From<Salle>()
                .Get();

            var inf = await Supabase
                .From<Infrastructure>()
                .Get();

            var Surf = await Supabase
                .From<Surface>()
                .Get();

            var p = new List<GeoPosition>();

            foreach (PositionGeo pos in pg.Models)
            {
                var point = GetPointOfInterest(pos, ph.Models, sl.Models, inf.Models, Surf.Models);

                if (point.Latitude != 0 && point.Longitude != 0)
                    p.Add(point);
            }

            return p;
        }
        catch (Exception)
        {
            return new List<GeoPosition>();
        }
    }

    public async Task<List<PositionGeo>> GetGeoPositions()
    {
        try
        {
            var p = await Supabase
                .From<PositionGeo>()
                .Get();

            foreach (PositionGeo pos in p.Models)
            {
                (double lon, double lat) position = ToLonLat(pos.Longitude, pos.Latitude);

                pos.Longitude = position.lon;
                pos.Latitude = position.lat;
            }

            return p.Models;
        }
        catch (Exception)
        {
            return new List<PositionGeo>();
        }
    }
    public async Task<List<PhotoDestination>> GetImInfras()
    {
        try
        {
            var p = await Supabase
                .From<PhotoDestination>()
                .Get();

            return p.Models;
        }
        catch (Exception)
        {
            return new List<PhotoDestination>();
        }
    }
    
    public async Task<List<Salle>> GetSalles()
    {
        try
        {
            var p = await Supabase
                .From<Salle>()
                .Get();

            return p.Models;
        }
        catch (Exception)
        {
            return new List<Salle>();
        }
    }

    public async Task<List<Infrastructure>> GetInfras()
    {
        try
        {
            var p = await Supabase
                .From<Infrastructure>()
                .Get();

            return p.Models;
        }
        catch (Exception)
        {
            return new List<Infrastructure>();
        }
    }
    
    public async Task<List<Surface>> GetSurfaces()
    {
        try
        {
            var p = await Supabase
                .From<Surface>()
                .Get();

            return p.Models;
        }
        catch (Exception)
        {
            return new List<Surface>();
        }
    }

}

