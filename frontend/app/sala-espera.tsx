import {
  View,
  Text,
  StyleSheet,
  SafeAreaView,
  TouchableOpacity,
  StatusBar,
  Dimensions,
  ScrollView,
  Image,
  Alert,
} from 'react-native';

import { router } from 'expo-router';

import { LinearGradient } from 'expo-linear-gradient';

import * as ScreenOrientation from 'expo-screen-orientation';

import { useEffect } from 'react';

const { width, height } = Dimensions.get('window');

export default function SalaEspera() {

  useEffect(() => {
    ScreenOrientation.lockAsync(
      ScreenOrientation.OrientationLock.LANDSCAPE
    );
  }, []);

  const codigoSala = '182507';

  const jugadores = [
    {
      id: 1,
      nombre: 'Isaboom',
      emoji: '😎',
    },

    {
      id: 2,
      nombre: 'Valemoon',
      emoji: '👑',
    },

    {
      id: 3,
      nombre: 'Maybaby',
      emoji: '🥰',
    },
  ];

  // CONFIRMAR SALIDA
  const salirSala = () => {

    Alert.alert(
      'Salir',
      '¿Socio está seguro de salir?',
      [
        {
          text: 'Cancelar',
          style: 'cancel',
        },

        {
          text: 'Sí, salir',
          onPress: () => router.push('/'),
        },
      ]
    );
  };

  return (
    <>
      <StatusBar hidden />

      <LinearGradient
        colors={['#12001f', '#1d022e', '#12001f']}
        style={styles.container}
      >

        <SafeAreaView style={styles.safe}>

          {/* CASITA */}
          <TouchableOpacity
            style={styles.homeButton}
            onPress={salirSala}
          >

            <Text style={styles.homeIcon}>
              🏠
            </Text>

          </TouchableOpacity>

          <ScrollView
            contentContainerStyle={styles.scroll}
            showsVerticalScrollIndicator={false}
          >

            {/* CARD */}
            <View style={styles.card}>

              {/* LOGO */}
              <Image
                source={require('./assets/images/logo.png')}
                style={styles.logoImage}
              />

              {/* TITULO */}
              <Text style={styles.subtitle}>
                Chachas Corporation
              </Text>

              {/* CONTENIDO */}
              <View style={styles.content}>

                {/* IZQUIERDA */}
                <View style={styles.leftSide}>

                  <Text style={styles.sectionTitle}>
                    Jugadores
                  </Text>

                  <View style={styles.playersRow}>

                    {jugadores.map((jugador) => (

                      <View
                        key={jugador.id}
                        style={styles.playerContainer}
                      >

                        <View style={styles.avatar}>

                          <Text style={styles.avatarEmoji}>
                            {jugador.emoji}
                          </Text>

                        </View>

                        <Text style={styles.playerName}>
                          {jugador.nombre}
                        </Text>

                      </View>

                    ))}

                  </View>

                </View>

                {/* DERECHA */}
                <View style={styles.rightSide}>

                  <Text style={styles.sectionTitle}>
                    Código
                  </Text>

                  <View style={styles.codeBox}>

                    <Text style={styles.codeText}>
                      {codigoSala}
                    </Text>

                  </View>

                </View>

              </View>

              {/* BOTON */}
              <TouchableOpacity
                style={styles.startButton}
                onPress={() => router.push('/juego')}
              >

                <Text style={styles.startText}>
                  Empezar partida
                </Text>

              </TouchableOpacity>

            </View>

          </ScrollView>

        </SafeAreaView>

      </LinearGradient>
    </>
  );
}

const styles = StyleSheet.create({

  container: {
    flex: 1,
  },

  safe: {
    flex: 1,
  },

  scroll: {
    flexGrow: 1,
    justifyContent: 'center',
    alignItems: 'center',
    paddingVertical: height * 0.02,
  },

  homeButton: {
    position: 'absolute',
    top: height * 0.03,
    left: width * 0.03,
    width: width * 0.06,
    height: width * 0.06,
    borderRadius: 100,
    backgroundColor: '#ff2ec4',
    justifyContent: 'center',
    alignItems: 'center',
    zIndex: 10,
  },

  homeIcon: {
    fontSize: width * 0.028,
  },

  card: {
    width: '94%',
    minHeight: height * 0.82,
    backgroundColor: '#220335',
    borderRadius: 35,
    paddingVertical: height * 0.03,
    paddingHorizontal: width * 0.04,
    alignItems: 'center',
  },

  logoImage: {
    width: width * 0.28,
    height: height * 0.11,
    resizeMode: 'contain',
    marginBottom: height * 0.01,
  },

  subtitle: {
    color: '#fff',
    fontSize: width * 0.02,
    marginBottom: height * 0.04,
    fontWeight: 'bold',
  },

  content: {
    width: '100%',
    flexDirection: 'row',
    justifyContent: 'space-between',
    marginBottom: height * 0.05,
  },

  leftSide: {
    width: '58%',
  },

  rightSide: {
    width: '35%',
    alignItems: 'center',
  },

  sectionTitle: {
    color: '#fff',
    fontSize: width * 0.022,
    fontWeight: 'bold',
    marginBottom: height * 0.025,
  },

  playersRow: {
    flexDirection: 'row',
    flexWrap: 'wrap',
  },

  playerContainer: {
    alignItems: 'center',
    marginRight: width * 0.03,
    marginBottom: height * 0.02,
  },

  avatar: {
    width: width * 0.085,
    height: width * 0.085,
    borderRadius: 100,
    backgroundColor: '#43106f',
    borderWidth: 3,
    borderColor: '#ff4dcf',
    justifyContent: 'center',
    alignItems: 'center',
  },

  avatarEmoji: {
    fontSize: width * 0.032,
  },

  playerName: {
    color: '#fff',
    marginTop: 8,
    fontSize: width * 0.014,
  },

  codeBox: {
    width: width * 0.22,
    height: height * 0.1,
    backgroundColor: '#fff',
    borderRadius: 15,
    justifyContent: 'center',
    alignItems: 'center',
  },

  codeText: {
    fontSize: width * 0.035,
    color: '#444',
    fontWeight: 'bold',
    letterSpacing: 4,
  },

  startButton: {
    width: width * 0.3,
    height: height * 0.08,
    backgroundColor: '#a93dc9',
    borderRadius: 18,
    justifyContent: 'center',
    alignItems: 'center',
    marginTop: height * 0.01,
  },

  startText: {
    color: '#fff',
    fontSize: width * 0.022,
    fontWeight: 'bold',
  },

});