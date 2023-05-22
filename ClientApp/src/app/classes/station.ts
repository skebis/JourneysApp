export interface Station {
  stationId?: string;
  idInt: number;
  nameFinnish: string;
  nameSwedish: string;
  nameEnglish: string;
  addressFinnish: string;
  addressSwedish: string;
  cityFinnish: string;
  citySwedish: string;
  operator: string;
  capacity: number;
  locationX: number;
  locationY: number;

  // departureCount and returnCount only come
  // as extra parameters from a single station get request.
  departureCount?: number;
  returnCount?: number;
}
